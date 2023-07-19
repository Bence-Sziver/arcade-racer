using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
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
        

        foreach (var wheel in wheels)
        {
            wheel.SteerAngle = Steer * maxSteer;
            wheel.Torque = Throttle * motorTorque;
        }
    }
}
