using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// A utility class providing methods for serializing and deserializing data using both binary and JSON formats.
    /// The class offers functionality to save and load serialized data to and from files within the persistent data path.
    /// </summary>
    public static class DataSerializer
    {
        /// <summary>
        /// Save item on persistent data path + defined file path (with extension)
        /// Ex: "/player/data.fun"
        /// </summary>
        public static void SerializeBinary<T>(T data, string filePathWithExtension)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string filePath = Path.Combine(Application.persistentDataPath, filePathWithExtension);

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(stream, data);
            }
        }

        /// <summary>
        /// Attempts to retrieve data on persistent data path + defined file path (with extension)
        /// Ex: "/player/data.fun"
        /// </summary>
        public static T DeserializeBinary<T>(string filePathWithExtension)
        {
            try
            {
                string filePath = Path.Combine(Application.persistentDataPath, filePathWithExtension);

                if (File.Exists(filePath))
                {
                    using (FileStream stream = new FileStream(filePath, FileMode.Open))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        T data = (T)formatter.Deserialize(stream);
                        return data;
                    }
                }

                else
                {
                    Debug.Log($"No file found at: {filePath}");
                    return default;
                }
            }
            catch (Exception e)
            {
                Debug.Log($"No file found at: {filePathWithExtension}");
                return default;
            }
        }



        /// <summary>
        /// Serializes the provided data using Unity's JsonUtility and saves it to a file at the specified path within the persistent data path.
        /// <para>File path format example: "/player/data.txt"</para>
        /// </summary>
        /// <typeparam name="T">Type of the data to be serialized.</typeparam>
        /// <param name="data">The data to be serialized and saved.</param>
        /// <param name="filePathWithExtension">The path (with extension) where the data will be saved within the persistent data path.</param>
        public static void SerializeJsonUtility<T>(T data, string filePathWithExtension)
        {
            string jsonData = JsonUtility.ToJson(data);
            string filePath = Path.Combine(Application.persistentDataPath, filePathWithExtension);

            File.WriteAllText(filePath, jsonData);
        }

        /// <summary>
        /// Deserializes data from a file at the specified path within the persistent data path using Unity's JsonUtility.
        /// <para>File path format example: "/player/data.txt"</para>
        /// </summary>
        /// <typeparam name="T">Type of the data to be deserialized.</typeparam>
        /// <param name="filePathWithExtension">The path (with extension) from which to load the data within the persistent data path.</param>
        /// <returns>The deserialized data, or the default value for type T if the file is not found or an error occurs during deserialization.</returns>
        public static T DeserializeJsonUtility<T>(string filePathWithExtension)
        {
            string filePath = Path.Combine(Application.persistentDataPath, filePathWithExtension);

            if (File.Exists(filePath))
            {
                try
                {
                    string jsonData = File.ReadAllText(filePath);
                    return JsonUtility.FromJson<T>(jsonData);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Could not deserialize object at: {filePath}. Error: {e.Message}");
                    return default;
                }
            }
            else
            {
                Debug.Log($"No file found at: {filePath}");
                return default;
            }
        }
    }
}