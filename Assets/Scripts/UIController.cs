using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject UIRacePanel;

    public Text UITextCurrentLap;
    public Text UITextCurrentTime;
    public Text UITextLastLapTime;
    public Text UITextBestLapTime;

    public Player UpdateUIForPlayer;

    private int currentLap;
    private float currentTime;
    private float lastLapTime;
    private float bestLapTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UpdateUIForPlayer == null)
        {
            return;
        }

        if (UpdateUIForPlayer.CurrentLap != currentLap)
        {
            currentLap = UpdateUIForPlayer.CurrentLap;
            UITextCurrentLap.text = $"LAP: {currentLap}";
        }
    }
}
