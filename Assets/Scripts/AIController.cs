using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject aiCar;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0, j = 0; i < GameManager.Instance.numberOfEnemies; i++, j += 10) {
            var car = GameObject.Instantiate(aiCar, new Vector3(-50, 5, -157 - j), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
