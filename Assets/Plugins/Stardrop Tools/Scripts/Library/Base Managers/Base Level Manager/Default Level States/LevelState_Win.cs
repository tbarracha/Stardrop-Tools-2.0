
using UnityEngine;

namespace StardropTools
{
    [System.Serializable]
    public class LevelState_Win : BaseLevelState
    {
        public LevelState_Win() : base("Win")
        {

        }

        public override void EnterState(IManagerState previousState)
        {
            base.EnterState(previousState);
            BaseEventManager.PlayResultEvents.OnWin?.Invoke();
        }
    }
}