using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public Transform _centerOfMass;
    public float motorTorque = 1500f;
    public int maxSpeed = 55;
    public float maxSteer = 20f;
    public float Throttle { get; set; }
    public float Steer { get; set; }

    private Rigidbody _rigidbody;
    private Wheel[] wheels;

    // Start is called before the first frame update
    void Start()
    {
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
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxSpeed);
        }
    }

    void FixedUpdate() {
        
    }
}
