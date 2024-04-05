using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : BaseWidget
{
    public delegate void FBuyButtonClickedSignature(TowerAsset asset);
    public event FBuyButtonClickedSignature BuyItemRequest;

    [SerializeField] private BuyButton ButtonPrefab;
    [SerializeField] private BuySO mScriptableObject;


    [SerializeField] private Button btnStartWave;
    [SerializeField] private Field txtMoney;
    [SerializeField] private Field txtWave;
    
    private GameController mController;
    [SerializeField] private Transform mButtonMenu;

    public void Init(GameController Controller)
    {
        mController = Controller;
        btnStartWave.onClick.AddListener(OnStartWaveButtonClick);
        txtMoney.mValue.text = ResourceManager.Instance.GetAvailableResources().ToString();

        foreach (var i in mScriptableObject.Towers)
        {
            BuyButton b = Instantiate(ButtonPrefab, mButtonMenu);
            b.OnBuyButtonClicked += OnBuyButtonClick;
            b.Init(i);
        }
    }

    public void UpdateWaveDetails(int wave)
    {
        txtWave.mValue.text = wave.ToString();
        btnStartWave.interactable = true;
    }
    private void OnStartWaveButtonClick()
    {
        Debug.Log("Start Wave Button Click");
        mController.WaveStartRequest();
        btnStartWave.interactable = false;

    }
    private void OnBuyButtonClick(TowerAsset asset)
    {
        BuyItemRequest?.Invoke(asset);
    }
}