
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class Utilities
{
    static Camera camera;

    public static List<T> GetItems<T>(Transform parent)
    {
        if (parent != null && parent.childCount > 0)
        {
            // list to store all detected components
            List<T> componentList = new List<T>();

            // loop to find components
            for (int i = 0; i < parent.childCount; i++)
            {
                var component = parent.GetChild(i).GetComponent<T>();
                if (component != null && componentList.Contains(component) == false)
                    componentList.Add(component);
            }

            // return array of found components
            return componentList;
        }

        else
        {
            Debug.Log("Parent has no children");
            return null;
        }
    }

    public static Transform CreateEmpty(string name, Vector3 position, Transform parent)
    {
        Transform point = new GameObject(name).transform;
        point.position = position;
        point.parent = parent;
        return point;
    }

    public static Vector3 ViewportRaycast(LayerMask layerMask)
    {
        if (camera == null)
            camera = Camera.main;

        Ray ray = camera.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 1000, layerMask);

        return hit.point;
    }

    public static List<Collider> HorizontalEightDirectionRaycast(Vector3 origin, float rayLength, LayerMask mask)
    {
        RaycastHit hit;
        Ray ray = new Ray();
        ray.origin = origin;

        List<Collider> colliders = new List<Collider>();

        // loop through directions
        // start at top and go clockwise
        for (int i = 0; i < 8; i++)
        {
            if (i == 0) // top
                ray.direction = Vector3.forward;

            else if (i == 1) // top Right
                ray.direction = Vector3.forward + Vector3.right;

            else if (i == 2) // right
                ray.direction = Vector3.right;

            else if (i == 3) // bottom Right
                ray.direction = Vector3.back + Vector3.right;

            else if (i == 4) // bottom
                ray.direction = Vector3.back;

            else if (i == 5) // bottom Left
                ray.direction = Vector3.back + Vector3.left;

            else if (i == 6) // left
                ray.direction = Vector3.left;

            else if (i == 7) // top Left
                ray.direction = Vector3.forward + Vector3.left;

            ray.direction *= rayLength;

            if (Physics.Raycast(ray, out hit, mask) && hit.collider != null)
                colliders.Add(hit.collider);
        }

        return colliders;
    }

    public static void StopCoroutine(Coroutine coroutine)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }

#if UNITY_EDITOR
    public static void ClearLog() //you can copy/paste this code to the bottom of your script
    {
        var assembly = System.Reflection.Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

    #region Gizmos

    public static void DrawCube(Vector3 position, Vector3 scale, Quaternion rotation)
    {
        Matrix4x4 cubeTransform = Matrix4x4.TRS(position, rotation, scale);
        Matrix4x4 oldGizmosMatrix = Gizmos.matrix;

        Gizmos.matrix *= cubeTransform;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
        Gizmos.matrix = oldGizmosMatrix;
    }

    public static void DrawString(string text, Vector3 worldPos, Color? color = null)
    {
        Handles.BeginGUI();
        if (color.HasValue) GUI.color = color.Value;
        var view = UnityEditor.SceneView.currentDrawingSceneView;
        Vector3 screenPos = view.camera.WorldToScreenPoint(worldPos);
        Vector2 size = GUI.skin.label.CalcSize(new GUIContent(text));
        GUI.Label(new Rect(screenPos.x - (size.x / 2), -screenPos.y + view.position.height + 4, size.x, size.y), text);
        Handles.EndGUI();
    }
    #endregion // gizmos

    #region Instantiate Prefabs
#if UNITY_EDITOR
    public static GameObject CreatePrefab(GameObject prefab)
        => PrefabUtility.InstantiatePrefab(prefab) as GameObject;

    public static GameObject CreatePrefab(GameObject prefab, Transform parent)
    {
        var obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        obj.transform.parent = parent;
        return obj;
    }

    public static T CreatePrefab<T>(GameObject prefab)
    {
        var obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        return obj.GetComponent<T>();
    }

    public static T CreatePrefab<T>(Object prefab)
    {
        var obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        return obj.GetComponent<T>();
    }

    /// <summary>
    /// Path to save ex: "Assets/Resources/SO" 
    /// </summary>
    /// <param name="className">Name of scriptable object class</param>
    /// <param name="path"> Path to save ex: "Assets/Resources/SO" </param>
    /// <returns></returns>
    public static void CreateScriptableObject(string soClassName, string path)
    {
        ScriptableObject so = ScriptableObject.CreateInstance(soClassName);

        AssetDatabase.CreateAsset(so, path);
        AssetDatabase.SaveAssets();
        Selection.activeObject = so;
    }

    public static T CreatePrefab<T>(GameObject prefab, Transform parent)
    {
        var obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        obj.transform.parent = parent;
        return obj.GetComponent<T>();
    }
#endif
    #endregion // instantiate prefabs
#endif
}