using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAIController : MonoBehaviour
{
    public Vector3 loc;
    public NavMeshAgent navMeshAgent;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        loc = new Vector3(-21, 0, -35);
        navMeshAgent.SetDestination(loc);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
