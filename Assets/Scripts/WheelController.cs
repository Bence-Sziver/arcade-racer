using UnityEngine;
using UnityEngine.InputSystem;

public class WheelController : MonoBehaviour
{
    public GameObject[] wheelsToRotate;
    public float rotationSpeed;
    private PlayerInput playerInput;
    private float acceleration;
    private float steering;
    private Animator animator;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        acceleration = moveInput.y;
        steering = moveInput.x;
        acceleration = Mathf.Clamp(acceleration, -1f, 1f);
        steering = Mathf.Clamp(steering, -1f, 1f);

        foreach (var wheel in wheelsToRotate)
        {
            wheel.transform.Rotate(Time.deltaTime * -acceleration * rotationSpeed, 0, 0, Space.Self);
        }

        if (steering > 0)
        {
            animator.SetBool("goingLeft", false);
            animator.SetBool("goingRight", true);
        }   else if (steering < 0)
        {
            animator.SetBool("goingRight", false);
            animator.SetBool("goingLeft", true);
        }   else
        {
            animator.SetBool("goingLeft", false);
            animator.SetBool("goingRight", false);
        }
    }
}
