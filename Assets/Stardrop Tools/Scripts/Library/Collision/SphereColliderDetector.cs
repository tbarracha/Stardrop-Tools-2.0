
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Detect colliders within sphere radius/bounds
    /// </summary>
    public class SphereColliderDetector : ColliderDetector
    {
        public float radius = 1;

        public override void ColliderSearch()
        {
            colliders = Physics.OverlapSphere(Position, radius, contactLayer);

            base.ColliderSearch();
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
            Gizmos.DrawSphere(Position, radius);
        }

#endif
    }
}