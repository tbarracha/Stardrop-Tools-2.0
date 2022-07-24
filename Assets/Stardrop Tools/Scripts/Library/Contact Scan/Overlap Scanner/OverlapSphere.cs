
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Lists colliders and events based on contact with filtered colliders
    /// </summary>
    public class OverlapSphere : OverlapScanner
    {
        [SerializeField] protected float radius = 1;

        public float Radius { get => radius; set => radius = value; }

        public override void OverlapScan() => SphereScan(Position);

        public void SphereScan(Vector3 position)
        {
            colliders = Physics.OverlapSphere(position + positionOffset, radius, contactLayers);
            ColliderCheck();
        }

        public void SphereScan(Vector3 position, float radius)
        {
            colliders = Physics.OverlapSphere(position + positionOffset, radius, contactLayers);
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
                Gizmos.DrawWireSphere(Position + positionOffset, radius);
            }
        }
#endif
    }
}


