using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public GameObject UIRacePanel;

    public Text UITextAllLaps;
    public Text UITextCurrentLap;
    public Text UITextCurrentLapTime;
    public Text UITextLastLapTime;
    public Text UITextBestLapTime;
    public Text UITextCarSpeed;
    public Text UITextFinished;
    public Text UITextPickup;

    public Player UpdateUIForPlayer;
    public GameObject UIPausePanel;
    public GameObject UIBoomPanel;
    private int maxLaps = -1;

    private int currentLap = -1;
    private float currentLapTime;
    private float lastLapTime;
    private float bestLapTime;
    private double carSpeed;
    private String pickup;
    Scene currentScene;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        UIPausePanel.SetActive(false);
        UIBoomPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (UpdateUIForPlayer == null)
        {
            return;
        }

        if (Time.timeScale == 0) {
            UIPausePanel.SetActive(true);
        }   else {
            UIPausePanel.SetActive(false);
        }

        if (currentScene.name == "SingleRace" && UpdateUIForPlayer.CurrentLap == GameManager.Instance.numberOfLaps + 1) {
            UITextFinished.text = $"You finished {UpdateUIForPlayer.PlayerPosition} place";
            StartCoroutine(QuittingCoroutine());
        }

        if (currentScene.name == "SingleRace" && GameManager.Instance.numberOfLaps != maxLaps) {
            maxLaps = GameManager.Instance.numberOfLaps;
            UITextAllLaps.text = $"/{maxLaps}";
        }

        if (UpdateUIForPlayer.controlType == Player.ControlType.HumanInput && UpdateUIForPlayer.IsBoom) {
            UIBoomPanel.SetActive(true);
        }

        if (UpdateUIForPlayer.IsBoom == false) {
            UIBoomPanel.SetActive(false);
        }

        if (UpdateUIForPlayer.CurrentLap != currentLap)
        {
            currentLap = UpdateUIForPlayer.CurrentLap;
            UITextCurrentLap.text = $"LAP: {currentLap}";
        }

        if (UpdateUIForPlayer.CurrentLapTime != currentLapTime)
        {
            currentLapTime = UpdateUIForPlayer.CurrentLapTime;
            UITextCurrentLapTime.text = $"TIME: {(int) currentLapTime / 60}:{currentLapTime % 60:00.000}";
        }

        if (UpdateUIForPlayer.LastLapTime != lastLapTime)
        {
            lastLapTime = UpdateUIForPlayer.LastLapTime;
            UITextLastLapTime.text = $"LAST: {(int)lastLapTime / 60}:{lastLapTime % 60:00.000}";
        }

        if (UpdateUIForPlayer.BestLapTime != bestLapTime)
        {
            bestLapTime = UpdateUIForPlayer.BestLapTime;
            UITextBestLapTime.text = bestLapTime < 1000000 ? $"BEST: {(int)bestLapTime / 60}:{bestLapTime % 60:00.000}" : "BEST: NONE";
        }

        if (UpdateUIForPlayer.CarSpeed != carSpeed)
        {
            carSpeed = UpdateUIForPlayer.CarSpeed;
            UITextCarSpeed.text = $"SPEED: {(int)carSpeed} km/h";
        }

        if (currentScene.name == "SingleRace" && UpdateUIForPlayer.Pickup != pickup) {
            pickup = UpdateUIForPlayer.Pickup;
            if (pickup == "") {
                UITextPickup.text = $"{pickup}";
            }   else {
                UITextPickup.text = $"{pickup} (Space)";
            }
        }
    }

    IEnumerator QuittingCoroutine() {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainMenu");
    }
}

