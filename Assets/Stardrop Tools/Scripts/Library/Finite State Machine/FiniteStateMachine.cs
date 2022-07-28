
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.FiniteStateMachine
{
    /// <summary>
    /// Class responsible for managing states
    /// </summary>
    public class FiniteStateMachine : BaseComponent
    {
        [SerializeField] protected int startIndex = 0;
        [SerializeField] protected IBaseState currentState;
        [SerializeField] protected IBaseState previousState;
        [SerializeField] protected float timeInCurrentState;
        [Space]
        [SerializeField] protected List<IBaseState> states;
        [SerializeField] protected bool debug;

        public override void Initialize()
        {
            base.Initialize();

            GetStateComponents();
            for (int i = 0; i < states.Count; i++)
                states[i].Initialize(this, i);

            ChangeState(startIndex);
        }

        public void UpdateStateMachine()
        {
            currentState.UpdateState();
            currentState.HandleInput();
        }

        public void ChangeState(BaseStateComponent nextState)
            => ChangeState(nextState.StateID);

        public void ChangeState(int nextStateID)
        {
            if (currentState != null && nextStateID == currentState.GetStateID())
            {
                if (debug)
                    Debug.Log("State is already: " + currentState.GetStateID());

                return;
            }

            if (currentState != null)
            {
                currentState.ExitState();
                previousState = currentState;
            }

            currentState = states[nextStateID];
            currentState.EnterState();

            if (debug && previousState != null)
                Debug.LogFormat("Changed stated from {0}, to {1}", previousState.GetStateID(), currentState.GetStateID());
        }


        public void GetStateComponents()
        {
            states = Utilities.GetItems<IBaseState>(transform);
            SetStateIDs();

            if (debug)
                Debug.Log("Detected " + states.Count + " states!");
        }

        public void SetStateIDs()
        {
            for (int i = 0; i < states.Count; i++)
                states[i].SetStateID(i);
        }


        /// <summary>
        /// State will be ID'ed based on entry order
        /// </summary>
        public void AddState(IBaseState newState)
        {
            if (states.Contains(newState) == false)
            {
                states.Add(newState);
                newState.SetStateID(states.Count - 1);
            }
        }

        /// <summary>
        /// States will be ID'ed based on entry order
        /// </summary>
        public void AddStates(IBaseState[] newStates)
        {
            for (int i = 0; i < newStates.Length; i++)
                AddState(newStates[i]);
        }


        public void RemoveState(IBaseState state)
        {
            if (states.Contains(state) == true)
                states.Remove(state);

            SetStateIDs();
        }

        public void RemoveState(int stateID)
        {
            if (states[stateID] != null)
                RemoveState(states[stateID]);

            SetStateIDs();
        }
    }
}