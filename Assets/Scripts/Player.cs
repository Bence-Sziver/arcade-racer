using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum ControlType { HumanInput, AI }
    public ControlType controlType = ControlType.HumanInput;

    public float BestLapTime { get; private set; } = Mathf.Infinity;
    public float LastLapTime { get; private set; } = 0;
    public float CurrentLapTime { get; private set; } = 0;
    public int CurrentLap { get; private set; } = 0;

    public double CarSpeed { get; private set; } = 0;

    private float lapTimerTimestamp;
    private int lastCheckpointPassed = 0;
    private Transform checkpointsParent;
    private int checkpointCount;
    private int checkpointLayer;
    private int barrierLayer;
    private CarController carController;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        // this is expensive, don't do in update()
        checkpointsParent = GameObject.Find("Checkpoints").transform;
        // checkpointCount = checkpointsParent.childCount;
        checkpointCount = 16;
        checkpointLayer = LayerMask.NameToLayer("checkpoint");
        barrierLayer = LayerMask.NameToLayer("barrier");
        carController = GetComponent<CarController>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CurrentLapTime = lapTimerTimestamp > 0 ? Time.time - lapTimerTimestamp : 0;

        if (controlType == ControlType.HumanInput)
        {
            carController.Steer = GameManager.Instance.InputController.SteerInput;
            carController.Throttle = GameManager.Instance.InputController.ThrottleInput;
        }   else {
            carController.Throttle = 1;
        }

        CarSpeed = _rigidbody.velocity.magnitude;
    }

    void StartLap()
    {
        Debug.Log("StartLap!");
        CurrentLap++;
        lastCheckpointPassed = 1;
        lapTimerTimestamp = Time.time;
    }

    void EndLap()
    {
        LastLapTime = Time.time - lapTimerTimestamp;
        BestLapTime = Mathf.Min(LastLapTime, BestLapTime);
        Debug.Log("EndLap! - Lap time was " + LastLapTime + " seconds");
    }

    private void OnTriggerEnter(Collider collider)
    {
        // if i'll want other triggers, ignore those
        if (collider.gameObject.layer != checkpointLayer && collider.gameObject.layer != barrierLayer)
        {
            return;
        }

        if (collider.gameObject.layer == barrierLayer)
        {
            _rigidbody.velocity = new Vector3(0,0,0);
            transform.position = new Vector3(GameObject.FindGameObjectsWithTag("Respawn")[lastCheckpointPassed - 1].transform.position.x, 2,
             GameObject.FindGameObjectsWithTag("Respawn")[lastCheckpointPassed - 1].transform.position.z);
             transform.localEulerAngles = new Vector3(0,0,0);
            return;
        }

        if (collider.gameObject.name == "1")
        {
            Debug.Log("lastCheckpointPassed:" + lastCheckpointPassed);
            Debug.Log("checkpointCount" + checkpointCount);
            // we just finished a lap
            if (lastCheckpointPassed == checkpointCount)
            {
                EndLap();
            }

            if (CurrentLap == 0 || lastCheckpointPassed == checkpointCount)
            {
                StartLap();
            }
            return;
        }

        // if we passed the next checkpoint in the sequence, update the lastest checkpoint
        if (collider.gameObject.name == (lastCheckpointPassed + 1).ToString())
        {
            lastCheckpointPassed++;
        }
    }
}
