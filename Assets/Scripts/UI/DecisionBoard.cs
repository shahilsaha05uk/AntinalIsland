using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecisionBoard : BaseWidget
{
    public TextMeshProUGUI lblDecisionText;

    public Button btnReturnToMainMenu;

    private void Start()
    {
        btnReturnToMainMenu.onClick.AddListener(ReturnToMainMenu);
    }

    public void Init(bool hasWon)
    {
        string str = (hasWon) ? "You Win!!!" : "You Lose!!";
        lblDecisionText.text = str;
    }
}
