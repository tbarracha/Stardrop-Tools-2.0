
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Lists colliders and events based on contact with filtered colliders
    /// </summary>
    public class OverlapBox : OverlapScanner
    {
        [SerializeField] protected Vector3 boxScale = Vector3.one;

        public Vector3 BoxScale { get => boxScale; set => boxScale = value; }

        public override void OverlapScan()
            => OverlapBoxScan(Position, Rotation);

        public void OverlapBoxScan(Vector3 position, Quaternion rotation)
        {
            colliders = Physics.OverlapBox(position + positionOffset, boxScale / 2, rotation, contactLayers);
            ColliderCheck();
        }

        public void OverlapBoxScan(Vector3 position, Vector3 scale, Quaternion rotation)
        {
            colliders = Physics.OverlapBox(position + positionOffset, scale / 2, rotation, contactLayers);
            ColliderCheck();
        }

#if UNITY_EDITOR
        [Header("Render")]
        [SerializeField] Color color = Color.red;
        [SerializeField] bool drawGizmos = true;

        private void OnDrawGizmos()
        {
            if (drawGizmos)
            {
                Gizmos.color = color;
                Utilities.DrawCube(Position + positionOffset, boxScale, Rotation);
            }
        }
#endif
    }

}