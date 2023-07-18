using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Car : MonoBehaviour
{
    public Transform _centerOfMass;
    public float motorTorque = 1500f;
    public float maxSteer = 20f;
    public float Throttle { get; set; }
    public float Steer { get; set; }

    private Rigidbody _rigidbody;
    private Wheel[] wheels;
    private PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = _centerOfMass.localPosition;
        wheels = GetComponentsInChildren<Wheel>();
    }

    // Update is called once per frame
    void Update()
    {
        Steer = GameManager.Instance.InputController.SteerInput;
        Throttle = GameManager.Instance.InputController.ThrottleInput;

        foreach (var wheel in wheels)
        {
            wheel.SteerAngle = Steer * maxSteer;
            wheel.Torque = Throttle * motorTorque;
        }
    }

    private void FixedUpdate()
    {
        /* Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        acceleration = moveInput.y;
        steer = moveInput.x;
        acceleration = Mathf.Clamp(acceleration, -1f, 1f);
        steer = Mathf.Clamp(steer, -1f, 1f);

        wheelColliderLeftRear.motorTorque = acceleration * motorTorque;
        wheelColliderRightRear.motorTorque = acceleration * motorTorque;

        wheelColliderLeftFront.steerAngle = steer * maxSteer;
        wheelColliderRightFront.steerAngle = steer * maxSteer; */
    }
}
