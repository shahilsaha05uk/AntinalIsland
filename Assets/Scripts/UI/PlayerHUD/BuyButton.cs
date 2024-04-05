using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public delegate void FOnBuyButtonClickedSignature(TowerAsset asset);
    public event FOnBuyButtonClickedSignature OnBuyButtonClicked;

    [SerializeField] private Image mbtnImage;
    [SerializeField] private Button mBtn;
    [SerializeField] private TextMeshProUGUI txtCost;
    private TowerAsset mAsset;

    public void Init(TowerAsset asset)
    {
        mAsset = asset;
        mbtnImage.sprite = asset.btnImage;
        txtCost.text = asset.Cost.ToString();
        mBtn.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        OnBuyButtonClicked?.Invoke(mAsset);
    }
}
