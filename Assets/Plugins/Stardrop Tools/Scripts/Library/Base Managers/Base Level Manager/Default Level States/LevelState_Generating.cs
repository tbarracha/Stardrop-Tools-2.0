
using UnityEngine;

namespace StardropTools
{
    [System.Serializable]
    public class LevelState_Generating : BaseLevelState
    {
        public LevelState_Generating() : base("Generating")
        {

        }

        public override void ExitState()
        {
            BaseEventManager.LevelEvents.OnLevelGenerationFinished?.Invoke();
        }
    }
}