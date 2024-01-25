
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// The CollisionSphere class specializes in detecting colliders within specified Sphere Radius by using Physics.OverlapSphere(),
    /// while also providing methods for detecting and filtering colliders based on criteria, and triggering events upon collision start and end events.
    /// </summary>
    public class CollisionSphere : ColliderDetector
    {
        public float radius = 1;

        public override void HandleUpdate()
        {
            ColliderDetection();
            base.HandleUpdate();
        }

        public override Collider[] ColliderDetection()
        {
            colliders = Physics.OverlapSphere(Position, radius, contactLayer);
            CheckForContact();
            return colliders;
        }

        public override System.Collections.Generic.List<Collider> FilteredColliderDetection()
        {
            ColliderDetection();
            return base.FilteredColliderDetection();
        }

#if UNITY_EDITOR
        [Header("Gizmos")]
        [SerializeField] Color gizmoColor = Color.red;
        [SerializeField] bool drawGizmos;

        private void OnDrawGizmos()
        {
            if (drawGizmos == false)
                return;

            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(Position, radius);
        }

#endif
    }
}