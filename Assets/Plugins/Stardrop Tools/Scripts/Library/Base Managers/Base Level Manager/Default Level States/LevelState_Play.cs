
using UnityEngine;

namespace StardropTools
{
    [System.Serializable]
    public class LevelState_Play : BaseLevelState
    {
        public LevelState_Play() : base("Play")
        {
            
        }

        public override void EnterState(IManagerState previousState)
        {
            base.EnterState(previousState);
            BaseEventManager.LevelEvents.OnLevelStarted?.Invoke();
        }
    }
}