using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    public InputController InputController { get; private set; }
    public int numberOfLaps;
    public int numberOfEnemies;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        InputController = GetComponentInChildren<InputController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        numberOfLaps = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
