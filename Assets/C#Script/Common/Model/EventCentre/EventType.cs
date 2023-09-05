namespace C_Script.Common.Model.EventCentre
{
    public enum CombatEventType
    {
        PlayerHurt,
        PlayerDeath,
        PlayerStop,
        PlayerStart,
        EnemyStart,
        EnemyStop,
        EnterBossRoom
    }

    public enum ScenesEventType
    {
        ReStart,
        GameOver,
        LevelPass
    }
}