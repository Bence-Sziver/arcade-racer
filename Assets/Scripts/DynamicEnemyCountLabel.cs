using UnityEngine;
using UnityEngine.UI;

public class DynamicEnemyCountLabel : MonoBehaviour
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
        GameManager.Instance.numberOfEnemies = (int) value;

    }
}