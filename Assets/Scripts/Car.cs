using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Car : MonoBehaviour
{
    public WheelCollider wheelColliderLeftFront;
    public WheelCollider wheelColliderRightFront;
    public WheelCollider wheelColliderLeftRear;
    public WheelCollider wheelColliderRightRear;

    public Transform wheelLeftFront;
    public Transform wheelRightFront;
    public Transform wheelLeftRear;
    public Transform wheelRightRear;

    public float motorTorque = 100f;
    public float maxSteer = 20f;

    private PlayerInput playerInput;
    private float acceleration;
    private float steer;

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
        var pos = Vector3.zero;
        var rot = Quaternion.identity;

        wheelColliderLeftFront.GetWorldPose(out pos, out rot);
        wheelLeftFront.position = pos;
        wheelLeftFront.rotation = rot;

        wheelColliderRightFront.GetWorldPose(out pos, out rot);
        wheelRightFront.position = pos;
        wheelRightFront.rotation = rot * Quaternion.Euler(0, 180, 0);

        wheelColliderLeftRear.GetWorldPose(out pos, out rot);
        wheelLeftRear.position = pos;
        wheelLeftRear.rotation = rot;

        wheelColliderRightRear.GetWorldPose(out pos, out rot);
        wheelRightRear.position = pos;
        wheelRightRear.rotation = rot * Quaternion.Euler(0, 180, 0);
    }

    private void FixedUpdate()
    {
        Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        acceleration = moveInput.y;
        steer = moveInput.x;
        acceleration = Mathf.Clamp(acceleration, -1f, 1f);
        steer = Mathf.Clamp(steer, -1f, 1f);

        wheelColliderLeftRear.motorTorque = acceleration * motorTorque;
        wheelColliderRightRear.motorTorque = acceleration * motorTorque;

        wheelColliderLeftFront.steerAngle = steer * maxSteer;
        wheelColliderRightFront.steerAngle = steer * maxSteer;
    }
}
