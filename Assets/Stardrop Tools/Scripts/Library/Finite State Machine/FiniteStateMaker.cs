
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.FiniteStateMachine
{
    public class FiniteStateMaker : MonoBehaviour
    {
        public FiniteStateMachine stateMachine;
        [Space]
        [SerializeField] string[] stateNames;
        [SerializeField] bool createState;
        [SerializeField] bool logStateEventsToConsole;

        public void CreateStates()
        {
            // No need to create states if we have created them in the inspector
            if (Application.isPlaying && stateNames.Length == stateMachine.StateCount)
                return;

            List<BaseState> baseStates = new List<BaseState>();
            for (int i = 0; i < stateNames.Length; i++)
                baseStates.Add(new BaseState(stateNames[i]));

            stateMachine.AddStates(baseStates.ToArray());
        }

        /// <summary>
        /// Log State events to the Unity Console so we can just Copy/Paste to any script
        /// </summary>
        public void LogStateEvents()
        {
            string start = "public BaseEvent "; // OnIdle;

            string name = "";               // Idle
            string on = "On";               // OnIdle
            string enter = "OnEnter";       // OnIdleEnter
            string update = "OnUpdate";     // OnIdleUpdate
            string exit = "OnExit";         // OnIdleExit

            string openGetState = " => gameStateMachine.GetState(";
            string stateIndex = "";
            string closeGetState = ").";

            string mainLog = "";

            for (int i = 0; i < stateNames.Length; i++)
            {
                name = stateNames[i];
                stateIndex = i.ToString();

                string logEnter = start + on + name + enter + openGetState + stateIndex + closeGetState + enter + ";";
                string logUpdate = start + on + name + update + openGetState + stateIndex + closeGetState + update + ";";
                string logExit = start + on + name + exit + openGetState + stateIndex + closeGetState + exit + ";";

                string log = logEnter + "\n" + logUpdate + "\n" + logExit + "\n";
                mainLog += log + "\n";
            }

            Debug.Log(mainLog);
        }



        private void OnValidate()
        {
            if (createState)
            {
                CreateStates();
                createState = false;
            }

            if (logStateEventsToConsole)
            {

                logStateEventsToConsole = false;
            }
        }
    }
}