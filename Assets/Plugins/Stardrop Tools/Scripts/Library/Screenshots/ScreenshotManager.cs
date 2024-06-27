using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace StardropTools.ScreenRecord
{
    public class ScreenshotManager : MonoBehaviour
    {
        [Header("List")]
        [SerializeField] List<GameObject> objectsToScreenshot = new List<GameObject>();
        [SerializeField] string screenshotFolder = "Assets/Screenshots";
        [SerializeField] KeyCode screenshotKey = KeyCode.S;

        [Header("Selected Camera")]
        [SerializeField] Camera selectedCamera; // Add this field to specify the camera

        [Header("Screenshot Dimensions")]
        [SerializeField] int screenshotWidth = 1920;
        [SerializeField] int screenshotHeight = 1080;

        [NaughtyAttributes.Button("Take Screenshot (All Objects)")]
        public void TakeScreenshot()
        {
            int id = 0;
            foreach (var obj in objectsToScreenshot)
            {
                TakeScreenshot(selectedCamera, $"screenshot_{id}.png", screenshotWidth, screenshotHeight);
                id++;
            }
        }

        public void TakeScreenshot(Camera camera, string fileName, int width, int height)
        {
            if (camera == null)
            {
                Debug.LogWarning("No camera selected for taking screenshots.");
                return;
            }

            // Store the original camera settings
            CameraClearFlags originalClearFlags = camera.clearFlags;
            Color originalBackgroundColor = camera.backgroundColor;

            // Set the camera's clear flags to solid color with a transparent background
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = new Color(0, 0, 0, 0); // Transparent background

            RenderTexture renderTexture = new RenderTexture(width, height, 100);

            // Render the object to the RenderTexture
            camera.targetTexture = renderTexture;
            camera.Render();

            // Activate the RenderTexture
            RenderTexture.active = renderTexture;

            // Create a Texture2D and read pixels from the RenderTexture
            Texture2D screenshot = new Texture2D(width, height, TextureFormat.RGBA32, false);
            screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            screenshot.Apply();

            // Ensure the screenshot folder exists
            if (!Directory.Exists(screenshotFolder))
            {
                Directory.CreateDirectory(screenshotFolder);
            }

            // Save the screenshot as a PNG file with transparency
            string filename = Path.Combine(screenshotFolder, fileName); // Use the provided filename
            System.IO.File.WriteAllBytes(filename, screenshot.EncodeToPNG());

            // Clean up
            camera.targetTexture = null;
            RenderTexture.active = null;
            TryDestroy(renderTexture);

            // Restore the camera's clear flags and background color
            camera.clearFlags = originalClearFlags;
            camera.backgroundColor = originalBackgroundColor;

#if UNITY_EDITOR
            // Refresh the Asset Database to see the new screenshot immediately in the Editor
            AssetDatabase.Refresh();
#endif

            Debug.Log("Screenshot saved as " + filename);
        }


        public static void TryDestroy(Object obj)
        {
            if (Application.isPlaying)
            {
                Destroy(obj);
            }
            else
            {
                DestroyImmediate(obj);
            }
        }
    }
}