
using UnityEngine;

namespace StardropTools
{
    public interface INavAgent
    {
        NavAgent NavAgentComponent { get; }
        Vector3 Destination { get; }
        Vector3 PathEndPoint { get; }

        Vector3 SetAgentDestination(Vector3 destination);
        void SetAgentPosition(Vector3 position);
        void StopAgent();
    }
}
