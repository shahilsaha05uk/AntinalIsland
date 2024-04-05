using UnityEngine;

public class GameEventTrigger : MonoBehaviour
{
    [Header("GameEvent")]
    [Tooltip("The name of the event you want to trigger")]
    public string EventName = "Load";

    [Tooltip("Additional data to send with the event")]
    public string AdditionalData = "";

    /// <summary>
    /// Trigger the specified GameEvent.
    /// </summary>
    public void TriggerGameEvent()
    {
        GameEvent.Trigger(EventName);
    }

    /// <summary>
    /// Trigger the specified GameEvent with additional data.
    /// </summary>
    public void TriggerGameEventWithAdditionalData()
    {
        GameEvent.Trigger(EventName + AdditionalData);
    }
}
