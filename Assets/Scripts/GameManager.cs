using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        numberOfLaps = 4;
        numberOfEnemies = 4;
        SceneManager.LoadScene("MainMenu");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
