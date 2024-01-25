
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// A base class for Level Managers that already has the states (while also obliging the implementation of the state methods):
    /// <para>Idle, Generating, Play, Paused, Win, Lose, Draw, Restarting, NextLevel</para>
    /// </summary>
    public abstract class BaseLevelManager : BaseManager<BaseLevelManager>
    {
        [SerializeField] protected string currentLevelStateName = string.Empty;
        [SerializeField] protected BaseLevelState currentLevelState;
        [SerializeField] protected List<BaseLevelState> levelStates;

        public BaseLevelState CurrentLevelState => currentLevelState;

        public static class DefaultLevelStates
        {
            public static readonly LevelState_Idle          Idle        = new LevelState_Idle();
            public static readonly LevelState_Generating    Generating  = new LevelState_Generating();
            public static readonly LevelState_Play          Playing     = new LevelState_Play();
            public static readonly LevelState_Paused        Paused      = new LevelState_Paused();
            public static readonly LevelState_Win           Win         = new LevelState_Win();
            public static readonly LevelState_Lose          Lose        = new LevelState_Lose();
            public static readonly LevelState_Draw          Draw        = new LevelState_Draw();
            public static readonly LevelState_Restarting    Restart     = new LevelState_Restarting();
            public static readonly LevelState_NextLevel     NextLevel   = new LevelState_NextLevel();
        }

        public override void Initialize()
        {
            base.Initialize();
            CreateStates();
        }

        protected virtual void CreateStates()
        {
            levelStates = new List<BaseLevelState>();

            levelStates.Add(DefaultLevelStates.Idle);
            levelStates.Add(DefaultLevelStates.Generating);
            levelStates.Add(DefaultLevelStates.Playing);
            levelStates.Add(DefaultLevelStates.Paused);
            levelStates.Add(DefaultLevelStates.Win);
            levelStates.Add(DefaultLevelStates.Lose);
            levelStates.Add(DefaultLevelStates.Draw);
            levelStates.Add(DefaultLevelStates.Restart);
            levelStates.Add(DefaultLevelStates.NextLevel);

            ChangeLevelState(DefaultLevelStates.Idle);
        }

        public override void GameStateChanged(GameState newGameState, GameState previousGameState)
        {
            switch (newGameState.StateName)
            {
                case BaseGameManager.DefaultGameStateNames.MainMenu:
                    ChangeLevelState(DefaultLevelStates.Idle);
                    break;

                case BaseGameManager.DefaultGameStateNames.Play:
                    ChangeLevelState(DefaultLevelStates.Playing);
                    break;

                case BaseGameManager.DefaultGameStateNames.Paused:
                    ChangeLevelState(DefaultLevelStates.Paused);
                    break;
            }
        }

        protected void ChangeLevelState(BaseLevelState targetState)
        {
            if (targetState == currentLevelState)
                return;

            if (currentLevelState != null)
                currentLevelState.ExitState();

            var prevState       = currentLevelState;
            currentLevelState   = targetState;
            currentLevelState.EnterState(prevState);

            currentLevelStateName = currentLevelState.StateName;
            LevelStateChanged(targetState, prevState);
            BaseEventManager.LevelEvents.OnLevelStateChanged?.Invoke(targetState);

            if (prevState != null)
                Debug.Log($"Level State Changed from {prevState.StateName} to {currentLevelState.StateName}");
            else
                Debug.Log($"Level State Changed to {currentLevelState.StateName}");
        }

        protected virtual void LevelStateChanged(BaseLevelState newLevelState, BaseLevelState previousState)
        {
            switch (newLevelState)
            {
                case LevelState_Idle:
                    break;

                case LevelState_Generating:
                    GenerateLevel();
                    break;

                case LevelState_Play:
                    if (previousState is LevelState_Paused)
                    {
                        ResumeLevel();
                        Debug.Log("Level Resumed!");
                    }
                    else if (previousState == null || previousState is LevelState_Idle || previousState is LevelState_Generating || previousState is LevelState_Restarting)
                    {
                        StartLevel();
                        Debug.Log("Level entered Play!");
                    }
                    break;

                case LevelState_Paused:
                    PauseLevel();
                    break;

                case LevelState_Win:
                    Win();
                    break;

                case LevelState_Lose:
                    Lose();
                    break;

                case LevelState_Draw:
                    Draw();
                    break;

                case LevelState_Restarting:
                    RestartLevel();
                    break;

                case LevelState_NextLevel:
                    NextLevel();
                    break;
            }
        }

        public abstract void GenerateLevel();
        public abstract void StartLevel();
        public abstract void ResumeLevel();
        public abstract void PauseLevel();
        public abstract void RestartLevel();
        public abstract void NextLevel();



        protected virtual void Win()
        {
            BaseEventManager.PlayResultEvents.OnWin?.Invoke();
        }

        protected virtual void Lose()
        {
            BaseEventManager.PlayResultEvents.OnLose?.Invoke();
        }

        protected virtual void Draw()
        {
            BaseEventManager.PlayResultEvents.OnDraw?.Invoke();
        }
    }
}