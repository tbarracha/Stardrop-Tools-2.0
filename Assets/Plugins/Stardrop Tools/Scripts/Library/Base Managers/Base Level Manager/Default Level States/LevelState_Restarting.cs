
using UnityEngine;

namespace StardropTools
{
    [System.Serializable]
    public class LevelState_Restarting : BaseLevelState
    {
        public LevelState_Restarting() : base("Restarting")
        {

        }

        public override void ExitState()
        {
            BaseEventManager.LevelEvents.OnLevelRestarted?.Invoke();
        }
    }
}