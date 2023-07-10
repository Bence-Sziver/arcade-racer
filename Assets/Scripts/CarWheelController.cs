using UnityEngine;
using UnityEngine.InputSystem;

// Using embedded actions with callbacks or reading values each frame.

public class CarWheelController : MonoBehaviour
{
    // the collection of wheels
    [SerializeField] private WheelCollider[] _wheelColliders;
    // torque
    [SerializeField] private float torque = 1200.0f;
    [SerializeField] private float _maxSteeringAngle = 30.0f;
    // maximum braking torque
    [SerializeField] private float _maxBrakingTorque = 550.0f;
    [SerializeField] private AudioClip skidSoundEffect;
    [SerializeField] private float _skidThreshold = 0.4f;
    private PlayerInput playerInput;
    private AudioSource _audioSource;
    private float braking;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        float acceleration = moveInput.y;
        float steering = moveInput.x;
        Move(acceleration, steering, braking);
    }

    private void Move(float acceleration, float steering, float braking)
    {
        // ensures the values are clamped
        acceleration = Mathf.Clamp(acceleration, -1f, 1f);
        steering = Mathf.Clamp(steering, -1f, 1f) * _maxSteeringAngle;
        braking = Mathf.Clamp(braking, -1f, 1f) * _maxBrakingTorque;
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
            
            // apply steering to the front wheels and braking to the rear
            if (i < 2)
            {
                _wheelColliders[i].steerAngle = steering;
            }   else
            {
                _wheelColliders[i].brakeTorque = braking;
            }
        }
        SkidCheck();
    }

    private void SkidCheck()
    {
        int skidCount = 0;
        for (int i = 0; i < _wheelColliders.Length; i++)
        {
            // get the point on the ground where the wheel collider is touching
            WheelHit wheelHit;
            _wheelColliders[i].GetGroundHit(out wheelHit);

            // check if there's any forward or sideway slipping
            if (Mathf.Abs(wheelHit.forwardSlip) >= _skidThreshold || Mathf.Abs(wheelHit.sidewaysSlip) >= _skidThreshold) {
                skidCount++;
                // check the sound isn't playing
                if (!_audioSource.isPlaying)
                {
                    // play the skidding sound effect
                    _audioSource.PlayOneShot(skidSoundEffect);
                }
            }
        }

        // turn off the skidding sound if there is no wheels skidding
        if (skidCount == 0 && _audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }

    public void Braking(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            braking = 1.0f;
        }   else if (context.canceled)
        {
            braking = 0.0f;
        }
    }
}
