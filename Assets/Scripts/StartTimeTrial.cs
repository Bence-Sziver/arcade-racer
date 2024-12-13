using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartTimeTrial : MonoBehaviour
{
    public Button buttonComponent;
    // Start is called before the first frame update
    void Start()
    {
        Button button = buttonComponent.GetComponent<Button>();
        button.onClick.AddListener(startTimeTrial);
    }

    void startTimeTrial() {
        SceneManager.LoadScene("TimeTrial");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
