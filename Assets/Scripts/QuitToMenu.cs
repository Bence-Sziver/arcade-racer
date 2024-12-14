using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitToMenu : MonoBehaviour
{
   public Button buttonComponent;
    // Start is called before the first frame update
    void Start()
    {
        Button button = buttonComponent.GetComponent<Button>();
        button.onClick.AddListener(quitToMenu);
    }

    void quitToMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
