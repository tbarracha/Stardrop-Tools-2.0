
using UnityEngine;
using UnityEngine.AI;

namespace StardropTools
{
    public class NavAgent : MonoBehaviour
    {
        [SerializeField] protected NavMeshAgent agent;

        public float RemainingDistance => agent.remainingDistance;
        public Vector3 Velocity => agent.velocity;

        public void SetSpeed(float speed) => agent.speed = speed;
        public void SetUpdateRotation(bool value) => agent.updateRotation = value;

        public void EnableNavmeshAgent(bool isEnabled)
        {
            if (agent.enabled == isEnabled)
                return;

            agent.enabled = isEnabled;
        }

        public void SetPositon(Vector3 position)
        {
            EnableNavmeshAgent(true);

            agent.nextPosition = position;
        }

        public void SetVelocity(Vector3 velocity)
        {
            EnableNavmeshAgent(true);

            agent.velocity = velocity;
        }

        public void SetDestination(Vector3 destination)
        {
            EnableNavmeshAgent(true);
            agent.isStopped = false;

            agent.SetDestination(destination);
        }

        public void StopAgent()
        {
            agent.isStopped = true;
            agent.velocity  = Vector3.zero;
            EnableNavmeshAgent(false);
        }
    }
}