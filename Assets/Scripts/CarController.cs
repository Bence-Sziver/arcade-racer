using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public Rigidbody sphereRb;
    public Rigidbody carRb;
    private PlayerInput playerInput;
    private float acceleration;
    private float steering;
    private bool isGrounded;

    public float speed;
    public float revSpeed;
    public float turnSpeed;
    public LayerMask groundLayer;
    private float normalDrag;
    public float modifiedDrag;
    public float alignToGroundTime;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Start is called before the first frame update
    void Start()
    {
        sphereRb.transform.parent = null;
        carRb.transform.parent = null;
        normalDrag = sphereRb.drag;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        acceleration = moveInput.y;
        steering = moveInput.x;
        acceleration = Mathf.Clamp(acceleration, -1f, 1f);
        steering = Mathf.Clamp(steering, -1f, 1f);

        if (isGrounded)
        {
            transform.Rotate(0, steering * turnSpeed * Time.deltaTime * acceleration, 0, Space.World);
        }

        acceleration *= acceleration > 0 ? speed : revSpeed;

        transform.position = sphereRb.transform.position;

        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f, groundLayer);

        Quaternion rotateTo = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, rotateTo, alignToGroundTime * Time.deltaTime);

        sphereRb.drag = isGrounded ? normalDrag : modifiedDrag;

    }

    private void FixedUpdate()
    {
        if (isGrounded) 
        {
            sphereRb.AddForce(transform.forward * acceleration, ForceMode.Acceleration);
        }   else
        {
            sphereRb.AddForce(transform.up * -30f);
        }

        carRb.MoveRotation(transform.rotation);
    }
}
