namespace StardropTools.GameStateManagement
{
    public enum StandardGameStates
    {
        NotFound = -1,
        Initializing,
        MainMenu,
        Play,
        Paused,
        GameOver, // GameOverCause: Win, Lose or Draw
        Loading,
        LevelTransition,
        Settings,
        Exit,
    }
}
