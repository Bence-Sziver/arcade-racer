using System;
using UnityEngine;
using UnityEngine.UI;

public class DynamicLapCountLabel : MonoBehaviour
{
 
    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        text.text = "4";
        GetComponentInParent<Slider>().onValueChanged.AddListener(HandleValueChanged);
    }

    private void HandleValueChanged(float value)
    {
        text.text = value.ToString();
        GameManager.Instance.numberOfLaps = (int) value;
    }
}