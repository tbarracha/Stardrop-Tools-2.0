
namespace StardropTools
{
    public abstract class BaseEventManager
    {
        public static class InitializationEvents
        {
            public static readonly CustomEvent OnInitialized = new CustomEvent();
            public static readonly CustomEvent OnPoolsInitialized = new CustomEvent();
        }

        public static class GameStateEvents
        {
            public static readonly CustomEvent<GameState, GameState> OnGameStateChanged           = new CustomEvent<GameState, GameState>();
            public static readonly CustomEvent<GameState>            OnRequestGameStateChange     = new CustomEvent<GameState>();
            public static readonly CustomEvent<string>               OnRequestGameStateNameChange = new CustomEvent<string>();
            public static readonly CustomEvent<int>                  OnRequestGameStateIDChange   = new CustomEvent<int>();

            public static readonly CustomEvent OnMainMenu = new CustomEvent();
            public static readonly CustomEvent OnPlayStart = new CustomEvent();
            public static readonly CustomEvent OnPlayEnd = new CustomEvent();
            public static readonly CustomEvent OnRestart = new CustomEvent();
                                                            
            public static readonly CustomEvent OnPaused = new CustomEvent();
            public static readonly CustomEvent OnResumed = new CustomEvent();
        }

        public static class PlayResultEvents
        {
            public static readonly CustomEvent OnWin = new CustomEvent();
            public static readonly CustomEvent OnLose = new CustomEvent();
            public static readonly CustomEvent OnDraw = new CustomEvent();
        }

        public static class LevelEvents
        {
            public static readonly CustomEvent<BaseLevelState> OnLevelStateChanged = new CustomEvent<BaseLevelState>();
            public static readonly CustomEvent<BaseLevelState> OnRequestLevelStateChanged = new CustomEvent<BaseLevelState>();

            public static readonly CustomEvent OnLevelStarted = new CustomEvent();
            public static readonly CustomEvent OnLevelRestarted = new CustomEvent();
            public static readonly CustomEvent OnLevelCompleted = new CustomEvent();
            public static readonly CustomEvent OnLevelGenerationFinished = new CustomEvent();
        }

        public static class SystemEvents
        {
            public static readonly CustomEvent OnGameSaved = new CustomEvent();
            public static readonly CustomEvent OnGameLoaded = new CustomEvent();
            public static readonly CustomEvent OnQuit = new CustomEvent();
        }
    }
}
