using System;
using UnityEngine;
using UnityEngine.AI;

namespace StardropTools
{
    public class NavAgent : MonoBehaviour, IStoppable, INavAgent
    {
        [SerializeField] protected NavMeshAgent agent;

        // Properties
        public float RemainingDistance => agent && agent.isActiveAndEnabled ? agent.remainingDistance : 0f;
        
        public Vector3 Velocity => agent && agent.isActiveAndEnabled ? agent.velocity : Vector3.zero;

        public Vector3 Destination => agent && agent.isActiveAndEnabled ? agent.destination : Vector3.zero;
        
        public Vector3 PathEndPoint => agent && agent.isActiveAndEnabled ? agent.pathEndPosition : Vector3.zero;

        public NavAgent NavAgentComponent => this;


        // Set speed of the agent
        public void SetSpeed(float speed)
        {
            if (agent)
                agent.speed = speed;
        }

        // Set whether the agent should update rotation
        public void SetUpdateRotation(bool value)
        {
            if (agent)
                agent.updateRotation = value;
        }

        // Enable or disable the NavMeshAgent
        public void EnableNavmeshAgent(bool isEnabled)
        {
            if (agent && agent.enabled != isEnabled)
                agent.enabled = isEnabled;
        }

        // Set the position of the agent
        public void SetAgentPosition(Vector3 position)
        {
            if (IsOnNavmesh())
            {
                EnableNavmeshAgent(true);
                agent.nextPosition = position;
            }
        }

        // Set the velocity of the agent
        public void SetVelocity(Vector3 velocity)
        {
            if (IsOnNavmesh())
            {
                EnableNavmeshAgent(true);
                agent.velocity = velocity;
            }
        }

        // Set the destination for the agent
        public Vector3 SetAgentDestination(Vector3 destination)
        {
            if (agent != null)
            {
                EnableNavmeshAgent(true);
                agent.isStopped = false;
                agent.SetDestination(destination);

                return agent.destination;
            }

            return destination;
        }

        // Stop the agent
        public void StopAgent()
        {
            if (IsOnNavmesh() && !agent.isStopped)
            {
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                EnableNavmeshAgent(false); 
            }
        }

        public void Stop()
        {
            StopAgent();
        }

        public bool IsOnNavmesh()
        {
            return agent && agent.isOnNavMesh;
        }

        public void PauseAgent()
        {
            if (IsOnNavmesh() && !agent.isStopped)
            {
                agent.isStopped = true;
            }
        }

        public void ResumeAgent()
        {
            if (IsOnNavmesh() && agent.isStopped)
            {
                agent.isStopped = false;
            }
        }
    }
}
