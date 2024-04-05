public struct SelectTowerEvent
{
    public Tower Tower;

    public SelectTowerEvent(Tower tower)
    {
        Tower = tower;
    }

    static SelectTowerEvent e;

    public static void Trigger(Tower tower)
    {
        e.Tower = tower;
        EventManager.TriggerEvent(e);
    }
}