using TMPro;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mLabel;
    public TextMeshProUGUI mValue;

    public void UpdateValue(string text)
    {
        mValue.text = text;
    }
}
