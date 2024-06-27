
using StardropTools.GameStateManagement;

namespace StardropTools
{
    public abstract class BaseGameEventManager : BaseEventManager
    {
        public static class GameStateEvents
        {
            /// <summary>
            /// CurrentState, PreviousState
            /// </summary>
            public static readonly EventCallback<GameState, GameState> OnGameStateChanged               = new EventCallback<GameState, GameState>();
            public static readonly EventCallback<GameState>            OnRequestGameStateChange         = new EventCallback<GameState>();
            public static readonly EventCallback<string>               OnRequestGameStateChangeByName   = new EventCallback<string>();
            public static readonly EventCallback<int>                  OnRequestGameStateChangeById     = new EventCallback<int>();

            public static readonly EventCallback OnPaused = new EventCallback();
            public static readonly EventCallback OnResumed = new EventCallback();

            public static readonly EventCallback<GameOverCause> OnGameOverRequest = new EventCallback<GameOverCause>();
            public static readonly EventCallback<GameOverCause> OnGameOver = new EventCallback<GameOverCause>();
        }

        public static class PlayResultEvents
        {
            public static readonly EventCallback OnWin  = new EventCallback();
            public static readonly EventCallback OnLose = new EventCallback();
            public static readonly EventCallback OnDraw = new EventCallback();
        }

        public static class LevelEvents
        {
            public static readonly EventCallback OnLevelGenerationFinished = new EventCallback();
            public static readonly EventCallback OnLevelStarted = new EventCallback();
            public static readonly EventCallback OnLevelRestarted = new EventCallback();
            public static readonly EventCallback OnLevelCompleted = new EventCallback();
            public static readonly EventCallback OnLevelFailed = new EventCallback();
        }
    }
}
