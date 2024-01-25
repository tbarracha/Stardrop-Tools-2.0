
using UnityEngine;

namespace StardropTools
{
    [System.Serializable]
    public class LevelState_Draw : BaseLevelState
    {
        public LevelState_Draw() : base("Draw")
        {

        }

        public override void EnterState(IManagerState previousState)
        {
            base.EnterState(previousState);
            BaseEventManager.PlayResultEvents.OnDraw?.Invoke();
        }
    }
}