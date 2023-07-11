using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public Rigidbody sphereRb;
    private PlayerInput playerInput;
    private float acceleration;
    private float steering;
    public float speed;
    public float revSpeed;
    public float turnSpeed;
    // public float maxSteerAngle;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Start is called before the first frame update
    void Start()
    {
        sphereRb.transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        acceleration = moveInput.y;
        steering = moveInput.x;
        acceleration = Mathf.Clamp(acceleration, -1f, 1f);
        steering = Mathf.Clamp(steering, -1f, 1f);
        acceleration *= acceleration > 0 ? speed : revSpeed;

        transform.position = sphereRb.transform.position;
        transform.Rotate(0, steering * turnSpeed * Time.deltaTime, 0, Space.World);
    }

    private void FixedUpdate()
    {
        sphereRb.AddForce(transform.forward * acceleration, ForceMode.Acceleration);
    }
}
