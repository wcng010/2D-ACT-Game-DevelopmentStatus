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
        EnterBossRoom,
        CriticalStrike
    }

    public enum ScenesEventType
    {
        ReStart,
        GameOver,
        LevelPass
    }
}