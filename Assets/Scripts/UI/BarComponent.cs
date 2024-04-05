using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarComponent : MonoBehaviour
{
    [SerializeField] private int min;
    [SerializeField] private int max;
    [SerializeField] private int current;
    public Slider mBar;
    public TextMeshProUGUI mlabel;
    private void Awake()
    {
        mBar.minValue = min;
        mBar.maxValue = max;
        mBar.value = current;
    }

    public void UpdateBar(int Value)
    {
        mBar.value = Value;
        mlabel.text = Value.ToString();
    }
}
