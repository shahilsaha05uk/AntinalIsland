using TMPro;
using UnityEngine;

public class ResourceDisplayUI : MonoBehaviour, EventListener<ResourceEvent>
{
    [SerializeField] private TextMeshProUGUI resourceText;

    public void OnEvent(ResourceEvent resourceEvent)
    {
        string resourceCount = resourceEvent.CurrentResources.ToString();
        resourceText.text = resourceCount;
    }

    protected virtual void OnEnable()
    {
        this.EventStartListening<ResourceEvent>();
    }

    protected virtual void OnDisable()
    {
        this.EventStopListening<ResourceEvent>();
    }
}
