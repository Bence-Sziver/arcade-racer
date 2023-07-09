using UnityEngine;
using UnityEngine.InputSystem;

// Using embedded actions with callbacks or reading values each frame.

public class CarController : MonoBehaviour
{
    // the collection of wheels
    [SerializeField] private WheelCollider[] _wheelColliders;
    // torque
    [SerializeField] private float torque = 1200.0f;
    [SerializeField] private float _maxSteeringAngle = 30.0f;
    // maximum braking torque
    [SerializeField] private float _maxBrakingTorque = 550.0f;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = playerInput.actions["Move"].ReadValue<float>();
        float steeringInput = playerInput.actions["Steer"].ReadValue<float>();
        // float brakingInput = playerInput.actions["Braking"]
        float acceleration = moveInput;
        float steering = steeringInput;
        Move(acceleration, steering);
    }

    private void Move(float acceleration, float steering)
    {
        // ensures the values are clamped
        acceleration = Mathf.Clamp(acceleration, -1f, 1f);
        steering = Mathf.Clamp(steering, -1f, 1f) * _maxSteeringAngle;
        // calculate the thrust torque
        float thrustTorque = acceleration * torque;

        // apply thrust torque to each wheel
        for (int i = 0; i < _wheelColliders.Length; i++)
        {
            _wheelColliders[i].motorTorque = thrustTorque;
            // get the position and rotation of the wheel collider
            _wheelColliders[i].GetWorldPose(out Vector3 position, out Quaternion quaternion);
            // reposition the game object with the mesh of the wheel
            // apply the rotation to the game object
            // wheel.transform.GetChild(0).transform.SetPositionAndRotation(position, quaternion);
            _wheelColliders[i].transform.GetChild(0).Rotate(-_wheelColliders[i].rpm / 60 * 360 * Time.deltaTime, 0, 0);
            
            if (i < 2)
            {
                _wheelColliders[i].steerAngle = steering;
            }
        }
    }
}
