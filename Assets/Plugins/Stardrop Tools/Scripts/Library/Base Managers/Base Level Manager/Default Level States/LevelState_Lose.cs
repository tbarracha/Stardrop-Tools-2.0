
using UnityEngine;

namespace StardropTools
{
    [System.Serializable]
    public class LevelState_Lose : BaseLevelState
    {
        public LevelState_Lose() : base("Lose")
        {

        }

        public override void EnterState(IManagerState previousState)
        {
            base.EnterState(previousState);
            BaseEventManager.PlayResultEvents.OnLose?.Invoke();
        }
    }
}