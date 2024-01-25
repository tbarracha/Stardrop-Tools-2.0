namespace StardropTools
{
    public abstract class ComponentTriggerParams : ComponentTrigger, ITriggableParams
    {
        public void TriggerBehaviour<T>(params T[] parameters)
        {
            foreach (ITriggable triggable in triggables)
            {
                if (triggable is ITriggableParams)
                    (triggable as ITriggableParams).TriggerBehaviour(parameters);
            }
        }
    }
}