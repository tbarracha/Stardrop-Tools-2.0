
using System.IO;
using UnityEngine;

namespace StardropTools.Tools
{
    [CreateAssetMenu(fileName = "New Enum Maker", menuName = "Stardrop / Tools / Enum Maker")]
    public class EnumMakerSO : ScriptableObject
    {
        [SerializeField] EnumContainer[] enumContainers;

        [NaughtyAttributes.Button("Create Enums")]
        public void CreateEnumsFromContainers()
        {
            for (int i = 0; i < enumContainers.Length; i++)
                enumContainers[i].CreateEnums();
        }
    }

    [System.Serializable]
    internal class EnumContainer
    {
        [SerializeField] string savePath;
        [SerializeField] string enumName;
        [SerializeField] string[] enums;

        public void CreateEnums()
        {
            // Don't do anything if array is empty
            if (enums.Exists() == false)
            {
                Debug.Log("Enum array is empty!");
                return;
            }

            // Convert to list and check for empty & duplicate values
            else
            {
                var enumList = Utilities.ToList(enums);

                Utilities.RemoveEmpty(enumList);
                Utilities.RemoveDuplicates(enumList);

                enums = enumList.ToArray();

                Debug.Log("Enums filtered");
            }

            // Create data path
            string path = savePath == string.Empty ? Application.streamingAssetsPath + "/Enums/" : savePath;
            Directory.CreateDirectory(path);

            // Create enum class file
            string enumClass = enumName + ".cs";
            string content =
                "\n" +
                "public enum " + enumName + "\n" +
                "{\n";

            // loop through array and add enums
            for (int i = 0; i < enums.Length; i++)
                content += "    " + enums[i] + ",\n";

            // close class
            content += "}";

            if (File.Exists(path + enumClass))
                File.Delete(path + enumClass);

            File.WriteAllText(path + enumClass, content);
            Debug.Log("Enums created! Minimize editor and reenter to view changes!");
        }
    }
}