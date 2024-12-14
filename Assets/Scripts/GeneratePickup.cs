using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePickup : MonoBehaviour
{
    PickupController parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.GetComponentInParent<PickupController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other) {
        parent.SpawnPickup(transform.position, transform.rotation);
        // other.gameObject.GetComponent<Player>().BestLapTime;
        Destroy(gameObject);
    }
}
