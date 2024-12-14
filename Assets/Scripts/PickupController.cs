using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    private float timer = 0f;
    private float respawnTime = 10f;
    private bool isActive = true;
    public GameObject newPickup;
    private UnityEngine.Vector3 position;
    private UnityEngine.Quaternion rotation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) {
            timer += Time.deltaTime;
            if (timer > respawnTime) {
                isActive = true;
                timer = 0;
                Instantiate(newPickup, position, rotation, this.transform);
            }
        }
    }

    public void SpawnPickup(UnityEngine.Vector3 pos, UnityEngine.Quaternion rot) {
        isActive = false;
        position = pos;
        rotation = rot;
    }
}
