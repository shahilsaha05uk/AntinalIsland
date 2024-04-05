using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    [SerializeField] private int startingResources = 100;

    private int _currentResources = 0;

    private void Start()
    {
        _currentResources = startingResources;
        ResourceEvent.Trigger(_currentResources);
    }

    public void AddResources(int amount)
    {
        _currentResources += amount;
        ResourceEvent.Trigger(_currentResources);
    }

    public void ConsumeResources(int amount)
    {
        _currentResources -= amount;
        ResourceEvent.Trigger(_currentResources);
    }

    public bool CanAffordPurchase(int cost)
    {
        return _currentResources >= cost;
    }

    public int GetAvailableResources() { return  _currentResources; }
}
