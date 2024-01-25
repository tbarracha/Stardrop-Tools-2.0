#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace StardropTools.Tools
{
    [CreateAssetMenu(fileName = "Animation Import Settings Automatic Loop Tagged Anims", menuName = "Stardrop / Tools / Animation Settings")]
    public class AnimationSettingsSO : ScriptableObject
    {
        const string infoMessage = "Edit at least One animation before pressing the button!";

        public GameObject[] fbxObjects;
        public string[] tagsToContain = { "idle", "walk", "run", "loop" };
        [Space]
        [NaughtyAttributes.ResizableTextArea][SerializeField] private string info;

        [NaughtyAttributes.Button("Set Animation Loop")]
        public void SetAnimationLoop()
        {
            if (fbxObjects == null || fbxObjects.Length == 0)
            {
                Debug.Log("Array is empty!");
                return;
            }

            foreach (var fbxFile in fbxObjects)
            {
                string assetPath = AssetDatabase.GetAssetPath(fbxFile);

                if (string.IsNullOrEmpty(assetPath))
                {
                    Debug.LogWarning($"GameObject {fbxFile.name} is not an asset.");
                    continue;
                }

                ModelImporter modelImporter = AssetImporter.GetAtPath(assetPath) as ModelImporter;

                if (modelImporter != null)
                {
                    ModelImporterClipAnimation[] clips = modelImporter.clipAnimations;

                    foreach (var clip in clips)
                    {
                        if (DoesClipNameContainTag(clip.name))
                        {
                            clip.loop       = true;
                            clip.loopTime   = true;
                            clip.loopPose   = true;

                            modelImporter.clipAnimations = clips;
                            AssetDatabase.ImportAsset(assetPath);
                            Debug.Log("Set loop for animation: " + clip.name);
                        }
                    }
                }
                else
                {
                    Debug.LogWarning($"Failed to load ModelImporter for {assetPath}");
                }
            }
        }

        bool DoesClipNameContainTag(string clipName)
        {
            clipName = clipName.ToLower();
            bool contains = false;

            for (int i = 0; i < tagsToContain.Length; i++)
            {
                if (clipName.Contains(tagsToContain[i]))
                {
                    contains = true;
                    break;
                }
            }

            return contains;
        }

        private void OnValidate()
        {
            if (info != infoMessage)
                info = infoMessage;
        }
    }
}
#endif
