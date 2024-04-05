using UnityEngine;

public class ResourceDebugger : MonoBehaviour
{
    [SerializeField] private ResourceManager resourceManager;
    [SerializeField] private int count = 1;

    [InspectorButton("AddResources")]
    public bool addResources;

    [InspectorButton("ConsumeResources")]
    public bool consumeResources;

    private void AddResources()
    {
        resourceManager.AddResources(count);
    }

    private void ConsumeResources()
    {
        if (resourceManager.CanAffordPurchase(count)) resourceManager.ConsumeResources(count);
    }
}
