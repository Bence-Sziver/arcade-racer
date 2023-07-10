using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public Rigidbody sphereRb;
    private PlayerInput playerInput;
    private float acceleration;
    public float speed;
    public float maxSteerAngle;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        float acceleration = moveInput.y;
        // float steering = moveInput.x;
        acceleration = Mathf.Clamp(acceleration, -1f, 1f);
        // steering = Mathf.Clamp(steering, -1f, 1f) * maxSteerAngle;

        transform.position = sphereRb.transform.position;
    }

    private void FixedUpdate()
    {
        sphereRb.AddForce(acceleration * transform.forward, ForceMode.Acceleration);
    }
}
