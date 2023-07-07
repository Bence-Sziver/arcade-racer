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
    [SerializeField] private float torque = 1200.0f;
    private PlayerInput playerInput;
    [SerializeField] private GameObject wheel;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        wheel = GameObject.Find("Front Left Wheel");
    }

    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = wheel.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        float radius = Vector3.Distance(transform.position, vertices[0]);
        Debug.Log(radius);
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = playerInput.actions["Move"].ReadValue<float>();

        float acceleration = moveInput;
        // float acceleration = moveInput.y;
        // float steering = moveInput.x;
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
