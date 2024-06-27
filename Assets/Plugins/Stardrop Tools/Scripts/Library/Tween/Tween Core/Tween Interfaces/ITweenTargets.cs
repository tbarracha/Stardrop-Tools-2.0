
using System.Collections.Generic;

namespace StardropTools.Tween
{
    public interface ITweenTargets<TValue, TTarget> : ITweenValue<TValue>
    {
        public bool Contains(TTarget target);

        public TTarget GetTarget();
        public List<TTarget> GetTargets();

        public bool AddTarget(TTarget target);
        public bool AddTargets(params TTarget[] targets);
        public bool AddTargets(List<TTarget> targets);


        public bool RemoveTarget(TTarget target);
        public bool RemoveTargets(params TTarget[] targets);
        public bool RemoveTargets(List<TTarget> targets);


        public void ClearTargets();



        /*
        public TTarget GetTarget()
        {
            return targets.Count > 0 ? targets[0] : default(TTarget);
        }

        public List<TTarget> GetTargets()
        {
            return targets;
        }


        public bool Contains(TTarget target)
        {
            return targets.Contains(target);
        }


        public bool AddTarget(TTarget target)
        {
            if (!Contains(target))
            {
                targets.Add(target);
                return true;
            }

            return false;
        }

        public bool AddTargets(params TTarget[] targets)
        {
            bool addedAny = false;

            foreach (TTarget iteratedTarget in targets)
            {
                if (AddTarget(iteratedTarget))
                {
                    addedAny = true;
                }
            }

            return addedAny;
        }

        public bool AddTargets(List<TTarget> targets)
        {
            bool addedAny = false;

            foreach (TTarget iteratedTarget in targets)
            {
                if (AddTarget(iteratedTarget))
                {
                    addedAny = true;
                }
            }

            return addedAny;
        }


        public bool RemoveTarget(TTarget target)
        {
            return targets.Remove(target);
        }

        public bool RemoveTargets(params TTarget[] targets)
        {
            bool removedAny = false;

            foreach (TTarget iteratedTarget in targets)
            {
                if (RemoveTarget(iteratedTarget))
                {
                    removedAny = true;
                }
            }

            return removedAny;
        }

        public bool RemoveTargets(List<TTarget> targets)
        {
            bool removedAny = false;

            foreach (TTarget iteratedTarget in targets)
            {
                if (RemoveTarget(iteratedTarget))
                {
                    removedAny = true;
                }
            }

            return removedAny;
        }


        public void ClearTargets()
        {
            targets.Clear();
        }
        */
    }
}
