using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeRace : MonoBehaviour
{
   public Button buttonComponent;
    // Start is called before the first frame update
    void Start()
    {
        Button button = buttonComponent.GetComponent<Button>();
        button.onClick.AddListener(resumeRace);
    }

    void resumeRace() {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
