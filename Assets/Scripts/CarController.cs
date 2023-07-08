using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Using embedded actions with callbacks or reading values each frame.

public class CarController : MonoBehaviour
{
    // the collection of wheels
    [SerializeField] private WheelCollider[] _wheelColliders;
    // torque
    [SerializeField] private float torque = 250.0f;
    private PlayerInput playerInput;

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
        float moveInput = playerInput.actions["Move"].ReadValue<float>();

        float acceleration = moveInput;
        Move(acceleration, 0, 0);
    }

    private void Move(float acceleration, float steering, float braking)
    {
        // ensures the values are clamped
        acceleration = Mathf.Clamp(acceleration, -1f, 1f);
        // calculate the thrust torque
        float thrustTorque = acceleration * torque;

        // apply thrust torque to each wheel
        foreach(var wheel in _wheelColliders)
        {
            wheel.motorTorque = thrustTorque;
        }
    }
}
