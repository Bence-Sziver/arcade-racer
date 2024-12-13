using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSingleRace : MonoBehaviour
{
    // Start is called before the first frame update
    public Button buttonComponent;
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        Button button = buttonComponent.GetComponent<Button>();
        button.onClick.AddListener(startSingleRace);
    }

    void startSingleRace() {
        SceneManager.LoadScene("SingleRace");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
