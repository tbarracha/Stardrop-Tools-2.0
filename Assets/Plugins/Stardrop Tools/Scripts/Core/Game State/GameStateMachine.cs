using System.Collections.Generic;

namespace StardropTools.GameStateManagement
{
    [System.Serializable]
    public class GameStateMachine : IGameStateMachine
    {
        [UnityEngine.SerializeField]
        private GameState currentState;
        [UnityEngine.SerializeField]
        private GameState previousState;
        [UnityEngine.SerializeField]
        private readonly List<GameState> gameStates;

        public int Id { get; set; }
        public GameState CurrentState => currentState;
        public GameState PreviousState => previousState;

        public readonly EventCallback<GameState, GameState> OnGameStateChanged;


        // Constructor
        // ---------------------------------------------------------------------------
        public GameStateMachine()
        {
            this.currentState = null;
            this.previousState = null;
            this.gameStates = new List<GameState>();

            OnGameStateChanged = new EventCallback<GameState, GameState>();
        }
        
        public GameStateMachine(GameState currentState)
        {
            this.currentState = currentState;
            this.previousState = null;
            this.gameStates = new List<GameState>();

            OnGameStateChanged = new EventCallback<GameState, GameState>();
        }

        public GameStateMachine(GameState currentState, List<GameState> states)
        {
            this.currentState = currentState;
            this.previousState = null;
            this.gameStates = states;

            OnGameStateChanged = new EventCallback<GameState, GameState>();
        }



        // Standard Game States
        // ---------------------------------------------------------------------------
        public void PopulateWithStandardGameStates()
        {
            var stateEnums = System.Enum.GetValues(typeof(StandardGameStates));
            foreach (var stateEnum in stateEnums)
            {
                RegisterState(stateEnum.ToString());
            }
        }

        public void RefreshStateIds()
        {
            for (int i = 0; i < gameStates.Count; i++)
            {
                gameStates[i].StateId = i;
            }
        }



        // Register States
        // ---------------------------------------------------------------------------
        public bool RegisterState(GameState state)
        {
            if (!gameStates.Contains(state))
            {
                state.PreviousStateId = -1;
                gameStates.Add(state);
                RefreshStateIds();
                return true;
            }
            return false;
        }

        public bool RegisterState(string stateName)
        {
            return RegisterState(new GameState(stateName));
        }



        // Unregister States
        // ---------------------------------------------------------------------------
        public bool UnregisterState(GameState state)
        {
            if (gameStates.Contains(state))
            {
                state.PreviousStateId = -1;
                gameStates.Remove(state);
                RefreshStateIds();
                return true;
            }
            return false;
        }

        public bool UnregisterState(string stateName)
        {
            return UnregisterState(new GameState(stateName));
        }



        // Change Game State
        // ---------------------------------------------------------------------------
        public bool ChangeGameState(GameState gameState)
        {
            if (gameStates.Contains(gameState))
            {
                previousState?.ExitState();

                previousState = currentState;   // Update previous state
                currentState = gameState;       // Change current state
                currentState.PreviousStateId = previousState != null ? previousState.StateId : -1;

                currentState.EnterState();
                OnGameStateChanged?.Invoke(currentState, previousState);

                return true;
            }
            return false;
        }

        public bool ChangeGameState(string stateName)
        {
            foreach (GameState state in gameStates)
            {
                if (state.StateName.Equals(stateName))
                {
                    return ChangeGameState(state);
                }
            }
            return false;
        }

        public bool ChangeGameState(int stateId)
        {
            if (stateId >= 0 && stateId < gameStates.Count)
            {
                return ChangeGameState(gameStates[stateId]);
            }
            return false;
        }

        public bool ChangeGameState(StandardGameStates standardGameState)
        {
            return ChangeGameState((int)standardGameState);
        }
    }
}
