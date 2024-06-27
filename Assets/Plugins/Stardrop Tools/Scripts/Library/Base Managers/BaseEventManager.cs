
using StardropTools.GameStateManagement;

namespace StardropTools
{
    public abstract class BaseEventManager
    {
        public static class InitializationEvents
        {
            public static readonly EventDelegate OnInitialized = new EventDelegate();
            public static readonly EventDelegate OnPoolsInitialized = new EventDelegate();
        }

        public static class GameStateEvents
        {
            /// <summary>
            /// CurrentState, PreviousState
            /// </summary>
            public static readonly EventDelegate<GameState, GameState> OnGameStateChanged               = new EventDelegate<GameState, GameState>();
            public static readonly EventDelegate<GameState>            OnRequestGameStateChange         = new EventDelegate<GameState>();
            public static readonly EventDelegate<string>               OnRequestGameStateChangeByName   = new EventDelegate<string>();
            public static readonly EventDelegate<int>                  OnRequestGameStateChangeById     = new EventDelegate<int>();

            public static readonly EventDelegate OnPaused = new EventDelegate();
            public static readonly EventDelegate OnResumed = new EventDelegate();

            public static readonly EventDelegate<GameOverCause> OnGameOverRequest = new EventDelegate<GameOverCause>();
            public static readonly EventDelegate<GameOverCause> OnGameOver = new EventDelegate<GameOverCause>();
        }

        public static class PlayResultEvents
        {
            public static readonly EventDelegate OnWin  = new EventDelegate();
            public static readonly EventDelegate OnLose = new EventDelegate();
            public static readonly EventDelegate OnDraw = new EventDelegate();
        }

        public static class LevelEvents
        {
            public static readonly EventDelegate OnLevelGenerationFinished = new EventDelegate();
            public static readonly EventDelegate OnLevelStarted = new EventDelegate();
            public static readonly EventDelegate OnLevelRestarted = new EventDelegate();
            public static readonly EventDelegate OnLevelCompleted = new EventDelegate();
            public static readonly EventDelegate OnLevelFailed = new EventDelegate();
        }

        public static class SystemEvents
        {
            public static readonly EventDelegate OnGameSaved = new EventDelegate();
            public static readonly EventDelegate OnGameLoaded = new EventDelegate();
            public static readonly EventDelegate OnQuit = new EventDelegate();
        }
    }
}
