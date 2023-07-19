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
        Vector3 dpos = cameraTarget.position + dist;
        Vector3 spos = Vector3.Lerp(transform.position, dpos, speed * Time.deltaTime);
        transform.position = spos;
        transform.LookAt(lookTarget.position);
    }
}
