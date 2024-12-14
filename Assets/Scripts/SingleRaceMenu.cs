using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SingleRaceMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Button buttonComponent;
    // Start is called before the first frame update
    void Start()
    {
        Button button = buttonComponent.GetComponent<Button>();
        button.onClick.AddListener(openSingleRaceMenu);
    }

    void openSingleRaceMenu() {
        // GameManager.Instance.NumberOfLaps = button.GetComponentInChildren<Text>().text;
        SceneManager.LoadScene("RaceSettings");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
