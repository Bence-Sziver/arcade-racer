using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public float ThrottleInput { get; private set; }
    public float SteerInput { get; private set; }

    private PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = playerInput.actions["Move"].ReadValue<Vector2>();
        ThrottleInput = moveInput.y;
        ThrottleInput = Mathf.Clamp(ThrottleInput, -1f, 1f);
        SteerInput = moveInput.x;
        SteerInput = Mathf.Clamp(SteerInput, -1f, 1f);
    }
}
