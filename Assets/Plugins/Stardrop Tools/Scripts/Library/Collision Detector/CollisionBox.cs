
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// The CollisionBox class specializes in detecting colliders within specified Box Dimensions by using Physics.OverlapBox(),
    /// while also providing methods for detecting and filtering colliders based on criteria, and triggering events upon collision start and end events.
    /// </summary>
    public class CollisionBox : ColliderDetector
    {
        public Vector3 boxDimensions = Vector3.one;
        [SerializeField] bool uniform;

        public override void HandleUpdate()
        {
            ColliderDetection();
            base.HandleUpdate();
        }

        public override Collider[] ColliderDetection()
        {
            colliders = Physics.OverlapBox(Position, boxDimensions * .5f, Rotation, contactLayer);
            CheckForContact();
            return colliders;
        }

        public override System.Collections.Generic.List<Collider> FilteredColliderDetection()
        {
            ColliderDetection();
            return base.FilteredColliderDetection();
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (uniform)
            {
                for (int i = 0; i < 3; i++)
                    boxDimensions[i] = boxDimensions[0];
            }
        }

#if UNITY_EDITOR
        [Header("Gizmos")]
        [SerializeField] Color gizmoColor = Color.red;
        [SerializeField] bool drawGizmos;

        private void OnDrawGizmos()
        {
            if (drawGizmos == false)
                return;

            Utilities.DrawCube(gizmoColor, Position, boxDimensions, Rotation);
        }

#endif
    }
}