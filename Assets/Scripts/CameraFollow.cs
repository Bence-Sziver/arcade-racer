using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraTarget;
    public float speed = 10;
    public Vector3 dist;
    public Transform lookTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void FixedUpdate()
    {
        Vector3 pos = cameraTarget.position + dist;
        Vector3 lerpPos = Vector3.Lerp(transform.position, pos,speed * Time.deltaTime);
        transform.position = lerpPos;
        transform.LookAt(lookTarget.position);
    }
}
