
using UnityEngine;

namespace StardropTools
{
    public class VelocityCalculator : BaseComponent, IFixedUpdateable
    {
        [SerializeField] Transform target;
        [SerializeField] Vector3 velocity;
        Vector3 prevPosition;

        public Vector3 Velocity => velocity;
        public bool IsFixedUpdating { get; private set; }

        public void SetTarget(Transform target) => this.target = target;

        public Vector3 CalculateVelocity()
        {
            if (target == null)
                return Vector3.zero;

            velocity = (target.position - prevPosition) / Time.deltaTime;
            prevPosition = target.position;
            return velocity;
        }

        // Return the velocity with its magnitude clamped by an input value
        public Vector3 GetClampedVelocity(float maxMagnitude)
        {
            Vector3 velocityClamped = CalculateVelocity();
            if (velocityClamped.magnitude > maxMagnitude)
                velocityClamped = velocityClamped.normalized * maxMagnitude;

            return velocityClamped;
        }

        public void StartFixedUpdate()
        {
            if (IsFixedUpdating)
                return;

            LoopManager.AddToUpdate(this);
            IsFixedUpdating = true;
        }

        public void StopFixedUpdate()
        {
            if (!IsFixedUpdating)
                return;

            LoopManager.RemoveFromUpdate(this);
            IsFixedUpdating = false;
        }

        public void HandleFixedUpdate()
        {
            CalculateVelocity();
        }

        public override void HandleUpdate()
        {
            CalculateVelocity();
        }



        [NaughtyAttributes.Button("Get Self")]
        void GetSelf()
        {
            if (target == null)
                target = transform;
        }
    }
}