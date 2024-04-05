public struct ResourceEvent
{
    public int CurrentResources;

    public ResourceEvent(int currentResources)
    {
        CurrentResources = currentResources;
    }

    static ResourceEvent e;

    public static void Trigger(int currentResources)
    {
        e.CurrentResources = currentResources;
        EventManager.TriggerEvent(e);
    }
}