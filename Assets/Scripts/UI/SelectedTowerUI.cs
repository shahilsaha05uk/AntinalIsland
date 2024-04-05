using TMPro;
using UnityEngine;

public class SelectedTowerUI : MonoBehaviour, EventListener<SelectTowerEvent>
{
    [SerializeField] private GameObject towerUIPanel;
    [SerializeField] private TextMeshProUGUI sellButtonText;

    private Tower selectedTower;

    private void Start()
    {
        towerUIPanel.SetActive(false);
    }

    public void OnEvent(SelectTowerEvent e)
    {
        // Check if a tower is selected
        if (e.Tower != null)
        {
            selectedTower = e.Tower;
            towerUIPanel.GetComponent<UIFollowGameObject>().SetTarget(e.Tower.transform);
            sellButtonText.text = $"Sell: {selectedTower.GetSellPrice()}";
            towerUIPanel.SetActive(true);
        }
        else
        {
            selectedTower = null;
            towerUIPanel.SetActive(false);
        }
    }

    protected virtual void OnEnable()
    {
        this.EventStartListening<SelectTowerEvent>();
    }

    protected virtual void OnDisable()
    {
        this.EventStopListening<SelectTowerEvent>();
    }

    public void UpgradeTower()
    {
        if (selectedTower != null)
        {
            selectedTower.UpgradeTower();
        }
    }

    public void SellTower()
    {
        if (selectedTower != null)
        {
            selectedTower.SellTower();
        }
    }

}