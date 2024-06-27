
using UnityEngine;

namespace StardropTools
{
    public static class RaycastUtils
    {
        static Camera camera;

        public static void GetCamera()
        {
            if (camera == null)
                camera = Camera.main;
        }

        public static Ray GetScreenPointToRay()
        {
            GetCamera();
            return camera.ScreenPointToRay(Input.mousePosition);
        }

        public static Vector3 GetColliderRaycastPosition(Collider collider, float rayDistance)
        {
            Ray ray = GetScreenPointToRay();
            if (collider.Raycast(ray, out RaycastHit result, rayDistance))
            {
                return result.point;
            }

            return Vector3.zero;
        }
    }
}
