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
    public bool IsFinished {get; private set;}
    public int PlayerPosition {get; private set;} = GameManager.Instance.numberOfEnemies;

    private float lapTimerTimestamp;
    private int lastCheckpointPassed = 0;
    private Transform checkpointsParent;
    private int checkpointCount;
    private int checkpointLayer;
    private int barrierLayer;
    private CarController carController;
    private Rigidbody _rigidbody;
    private float lastError;
    private float integral;
    private float turboMaxTime = 5f;
    private float turboCurrentTime;
    private bool isTurboTime;
    private bool firstTurbo = true;

    public String Pickup {get; set;} = "";
    public GameObject oil;
    public GameObject missile;

    private void Awake()
    {
        // this is expensive, don't do in update()
        checkpointsParent = GameObject.Find("Checkpoints").transform;
        // checkpointCount = checkpointsParent.childCount;
        checkpointCount = 36;
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
            carController.Throttle = 1f;
            carController.Steer = AIControl();
        }

        CarSpeed = _rigidbody.velocity.magnitude;
        if (GameManager.Instance.InputController.IsResetPressed) {
            ResetPositionByKey();
        }

        if (GameManager.Instance.InputController.IsEscapePressed) {
            PauseMenu();
        }
        if (Pickup == "turbo") {
            Turbo();
            if (isTurboTime) {
                turboCurrentTime += Time.deltaTime;
                if (turboCurrentTime > turboMaxTime) {
                    isTurboTime = false;
                    turboCurrentTime = 0f;
                    Pickup = "";
                    firstTurbo = true;
                }
            }
        }
        if (Pickup == "oil") {
            Oil();
        }
        if (Pickup == "torpedo") {
            Torpedo();
        }
        if (Pickup == "bomb") {
            Bomb();
        }
    }

    void Turbo() {
        if (firstTurbo && GameManager.Instance.InputController.IsSpacePressed) {
            isTurboTime = true;
            firstTurbo = !firstTurbo;
            _rigidbody.velocity *= 10f;
        }
    }

    void Oil() {
        if (GameManager.Instance.InputController.IsSpacePressed) {
            Pickup = "";
            Instantiate(oil);
        }
    }

    void Torpedo() {
        if (GameManager.Instance.InputController.IsSpacePressed) {
            Pickup = "";
            Instantiate(missile);
        }
    }

    void Bomb() {
        Pickup = "";

    }

    float AIControl() {
        float error = CalcError();
        return Pid(error);
    }

    private float CalcError()
    {
        Vector3 currentOrientation = transform.forward;
        currentOrientation.y = 0;
        Transform nextWaypoint;
        if (lastCheckpointPassed == 36) {
             nextWaypoint = GameObject.FindGameObjectsWithTag("Respawn")[1].transform;
        }   else {
            nextWaypoint = GameObject.FindGameObjectsWithTag("Respawn")[lastCheckpointPassed].transform;
        }
        Vector3 diff = nextWaypoint.position - transform.position;
        diff.y = 0;
        float error = Vector3.SignedAngle(currentOrientation, diff, new Vector3(0, 1, 0));
        return error;
    }
     private float Pid(float error)
    {
        float proportionalValue = 0.03f * error;
        integral += error * Time.fixedDeltaTime;
        float integralValue = 0.0f * integral;
        float derivative = (error - lastError) / Time.fixedDeltaTime;
        float derivativeValue = 0.01f * derivative;
        lastError = error;

        return Mathf.Clamp(proportionalValue + integralValue + derivativeValue, -1f, 1f);

    }   

    void ResetPositionByKey() {
         _rigidbody.velocity = new Vector3(0,0,0);
            var lastCheckPoint = GameObject.FindGameObjectsWithTag("Respawn")[lastCheckpointPassed - 1];
            var xCoord = lastCheckPoint.transform.position.x;
            var zCoord = lastCheckPoint.transform.position.z;
            transform.position = new Vector3(xCoord, 2, zCoord);
    }

    void ResetAIPosition() {
        _rigidbody.velocity = new Vector3(0,0,0);
            var lastCheckPoint = GameObject.FindGameObjectsWithTag("Respawn")[lastCheckpointPassed - 1];
            var xCoord = lastCheckPoint.transform.position.x;
            var zCoord = lastCheckPoint.transform.position.z;
            transform.position = new Vector3(xCoord, 2, zCoord);
    }

    void PauseMenu() {
			Time.timeScale = 0;
	}

    void StartLap()
    {
        CurrentLap++;
        lastCheckpointPassed = 1;
        lapTimerTimestamp = Time.time;
    }

    void EndLap()
    {
        LastLapTime = Time.time - lapTimerTimestamp;
        BestLapTime = Mathf.Min(LastLapTime, BestLapTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        // más triggerekhez
        if (collider.gameObject.layer != checkpointLayer && collider.gameObject.layer != barrierLayer && collider.gameObject.tag != "Pickup")
        {
            return;
        }

        if (Pickup != "" && collider.gameObject.tag == "Pickup") {
            int rand = UnityEngine.Random.Range(0, 3);
            if (rand == 0) {
                Pickup = "turbo";
            }   else if (rand == 1) {
                Pickup = "oil";
            }   else if (rand == 2) {
                Pickup = "torpedo";
            }   else if (rand == 3) {
                Pickup = "bomb";
            }
            Debug.Log(Pickup);
            return;
        }

        if (collider.gameObject.layer == barrierLayer)
        {
            _rigidbody.velocity = new Vector3(0,0,0);
            var lastCheckPoint = GameObject.FindGameObjectsWithTag("Respawn")[lastCheckpointPassed - 1];
            var xCoord = lastCheckPoint.transform.position.x;
            var zCoord = lastCheckPoint.transform.position.z;
            transform.position = new Vector3(xCoord, 2, zCoord);
            //  transform.localEulerAngles = new Vector3(0,0,0);
            return;
        }

        if (collider.gameObject.name == "1")
        {
            // kör teljesítve
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

        if (collider.gameObject.name == (lastCheckpointPassed + 1).ToString())
        {
            lastCheckpointPassed++;
        }
    }
}
