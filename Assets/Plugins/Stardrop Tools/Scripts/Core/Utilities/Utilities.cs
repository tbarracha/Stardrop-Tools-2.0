
using System.Collections.Generic;
using System.IO;
using System.Linq;
using StardropTools;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace StardropTools
{

    /// <summary>
    /// Class that contains miscellanious static utilities
    /// </summary>
    public static class Utilities
    {
        static Camera camera;

        public static readonly bool[] TrueAndFalse = { true, false };
        public static readonly int[] OneAndNegativeOne = { 1, -1 };

        private static System.Random random = new System.Random();

        #region Log & Debug

#if UNITY_EDITOR
        public static void ClearLog() //you can copy/paste this code to the bottom of your script
        {
            var assembly = System.Reflection.Assembly.GetAssembly(typeof(Editor));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Log(object message, Object context = null)
        {
            var stackTrace = new System.Diagnostics.StackTrace(true);
            var frame = stackTrace.GetFrame(1);

            string fileName = frame.GetFileName();
            int lineNumber = frame.GetFileLineNumber();

            string logMessage = $"{message} ({fileName}:{lineNumber})";

            Debug.Log(logMessage, context);
        }

        public static string ColorString(string message, string color)
        {
            return $"<color={color}>{message}</color>";
        }

#endif
        #endregion // log & Debug


        #region Misc

        public static AnimationCurve TwoPointCurve(float valueA, float valueB)
        {
            AnimationCurve curve = new AnimationCurve();
            curve.AddKey(0, valueA);
            curve.AddKey(1, valueB);

            return curve;
        }

        public static AnimationCurve FlatZeroCurve()
            => TwoPointCurve(0, 0);

        public static AnimationCurve FlatOneCurve()
            => TwoPointCurve(1, 1);

        public static string SetArrayIndexBeforeName(string name, char spliter, int index)
        {
            string[] parts = name.Trim().Split(spliter);
            if (parts.Length > 0)
            {
                name = $"{index} - {parts.GetLast().Trim()}";
            }
            else
            {
                name = $"{index} - {name}";
            }

            return name;
        }

        #endregion // Misc


        #region Primitive Types

        /// <summary>
        /// Converts float to int
        /// </summary>
        public static int ToInt(this float value) => Mathf.CeilToInt(value);

        /// <summary>
        /// Converts int to float
        /// </summary>
        public static float ToFloat(this int value) => value;



        /// <summary>
        /// 0 = False, 1 (or any other value) = True
        /// </summary>
        public static bool ConvertIntToBool(int id)
        {
            if (id == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 0 - False, 1 - True
        /// </summary>
        public static int ConvertBoolToInt(bool value)
        {
            if (value == false)
                return 0;
            else
                return 1;
        }


        /// <summary>
        /// 1 - False, 0 - True
        /// </summary>
        public static int InverseConvertBoolToInt(bool value)
        {
            if (value == false)
                return 1;
            else
                return 0;
        }

        /// <summary>
        /// String's first character becomes Uppercase
        /// </summary>
        public static string ToUpperFirstLetter(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            str.ToLower();
            char[] characters = str.ToCharArray();
            characters[0] = char.ToUpper(characters[0]);

            return new string(characters);
        }

        #endregion // Primitive Types


        #region Base Component

        /// <summary>
        /// Invokes the InitializeManager() method on an array of IManager
        /// </summary>
        public static void InitializeManagers(IManager[] managers)
        {
            if (managers.Exists())
                for (int i = 0; i < managers.Length; i++)
                    managers[i].InitializeManager();
        }

        /// <summary>
        /// Invokes the LateInitializeManager() method on an array of IManager
        /// </summary>
        public static void LateInitializeManagers(IManager[] managers)
        {
            if (managers.Exists())
                for (int i = 0; i < managers.Length; i++)
                    managers[i].LateInitializeManager();
        }



        /// <summary>
        /// Invokes the Initialize() method on a List of BaseComponents
        /// </summary>
        public static void Initialize(List<IInitializeable> initializeables)
        {
            if (initializeables.Exists())
                for (int i = 0; i < initializeables.Count; i++)
                    initializeables[i].Initialize();
        }

        /// <summary>
        /// Invokes the Initialize() method on an Array of BaseComponents
        /// </summary>
        public static void Initialize(params IInitializeable[] initializeables)
        {
            if (initializeables.Exists())
            {
                for (int i = 0; i < initializeables.Length; i++)
                    initializeables[i].Initialize();
            }
            else
            {
                Debug.Log("Array is empty!");
            }
        }



        /// <summary>
        /// Invokes the LateInitialize() method on an Array of BaseComponents
        /// </summary>
        public static void LateInitialize(ILateInitializeable[] lateInitializeables)
        {
            if (lateInitializeables.Exists())
            {
                for (int i = 0; i < lateInitializeables.Length; i++)
                    lateInitializeables[i].LateInitialize();
            }
            else
            {
                Debug.Log("Array is empty!");
            }
        }

        /// <summary>
        /// Invokes the LateInitialize() method on a List of BaseComponents
        /// </summary>
        public static void LateInitialize(List<ILateInitializeable> lateInitializers)
        {
            if (lateInitializers.Exists())
                for (int i = 0; i < lateInitializers.Count; i++)
                    lateInitializers[i].LateInitialize();
        }


        /// <summary>
        /// Invokes the StartUpdate() method on an Array of BaseComponents
        /// </summary>
        public static void StartUpdateOf(IUpdateable[] updateables)
        {
            if (updateables.Exists())
                for (int i = 0; i < updateables.Length; i++)
                    updateables[i].StartUpdate();
        }

        /// <summary>
        /// Invokes the StartUpdate() method on a List of BaseComponents
        /// </summary>
        public static void StartUpdateOf(List<IUpdateable> updateables)
        {
            if (updateables.Exists())
                for (int i = 0; i < updateables.Count; i++)
                    updateables[i].StartUpdate();
        }


        /// <summary>
        /// Invokes the StopUpdate() method on an Array of BaseComponents
        /// </summary>
        public static void StopUpdateOf(IUpdateable[] updateables)
        {
            if (updateables.Exists())
                for (int i = 0; i < updateables.Length; i++)
                    updateables[i].StopUpdate();
        }

        /// <summary>
        /// Invokes the StopUpdate() method on a List of BaseComponents
        /// </summary>
        public static void StopUpdateOf(List<IUpdateable> updateables)
        {
            if (updateables.Exists())
                for (int i = 0; i < updateables.Count; i++)
                    updateables[i].StopUpdate();
        }

        #endregion // Base Component


        #region Random

        /// <summary>
        /// Randomly returns True or False
        /// </summary>
        public static bool RandomTrueOrFalse() => TrueAndFalse.GetRandom();


        /// <summary>
        /// Randomly returns 1 or -1
        /// </summary>
        public static int RandomOneMinus() => OneAndNegativeOne.GetRandom();


        /// <summary>
        /// Returns a random item between ItemOne or ItemTwo
        /// </summary>
        public static T GetRandomBetween<T>(T itemOne, T itemTwo)
        {
            T[] array = new T[2];
            array[0] = itemOne;
            array[1] = itemTwo;

            return array.GetRandom();
        }


        public static int GetEnumLength<T>() where T : System.Enum
            => System.Enum.GetValues(typeof(T)).Length;

        /// <summary>
        /// Returns a random enum of enumType
        /// </summary>
        public static T GetRandomEnumValue<T>() where T : System.Enum
        {
            System.Random random = new System.Random();
            System.Array values = System.Enum.GetValues(typeof(T));

            return (T)values.GetValue(random.Next(values.Length));
        }


        /// <summary>
        /// Returns an array of random, non repeating enums of enumType
        /// </summary>
        public static T[] GetRandomEnumValuesNonRepeat<T>(int count) where T : System.Enum
        {
            System.Random random = new System.Random();

            if (count > System.Enum.GetValues(typeof(T)).Length)
            {
                Debug.LogWarning("Requested count is greater than the number of available enum values.");
                return null;
            }

            List<T> allValues = System.Enum.GetValues(typeof(T)).Cast<T>().ToList();
            List<T> selectedValues = new List<T>();

            for (int i = 0; i < count; i++)
            {
                int randomIndex = random.Next(allValues.Count);
                selectedValues.Add(allValues[randomIndex]);
                allValues.RemoveAt(randomIndex);
            }

            return selectedValues.ToArray();
        }

        #endregion // Random


        #region Arrays & Lists

        #region Array

        /// <summary>
        /// Returns true if Index is within array bounds
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool IsIndexWithinBounds<T>(this T[] array, int index)
        {
            return index >= 0 && index < array.Length;
        }


        /// <summary>
        /// Returns the FIRST element inside the array
        /// </summary>
        public static T GetFirst<T>(this T[] array) => array[0];

        /// <summary>
        /// Returns the LAST element inside the array
        /// </summary>
        public static T GetLast<T>(this T[] array) => array[array.Length - 1];

        /// <summary>
        /// Returns => array.Length - 1;
        /// </summary>
        public static int GetLastIndex<T>(this T[] array) => array.Length - 1;


        public static bool IsLastIndex<T>(this T[] array, int index)
        {
            if (index >= array.GetLastIndex())
                return true;
            else
                return false;
        }

        /// <summary>
        /// Extension method for arrays that returns the index of the next element in a circular manner.
        /// If the input array is null or empty, an error is logged, and an invalid index value (-1) is returned.
        /// </summary>
        public static int GetNextIndex<T>(this T[] array, int currentIndex)
        {
            if (array == null || array.Length == 0)
            {
                Debug.LogError("Array is null or empty!");
                return -1; // Return an invalid index value
            }

            int nextIndex = (currentIndex + 1) % array.Length;
            return nextIndex;
        }

        public static int GetNextIndexClamped<T>(this T[] array, int currentIndex)
        {
            if (array == null || array.Length == 0)
            {
                Debug.LogError("Array is null or empty!");
                return -1; // Return an invalid index value
            }

            int nextIndex = Mathf.Clamp(currentIndex + 1, 0, array.Length - 1);
            return nextIndex;
        }

        public static int GetEmptyIndex<T>(this T[] array)
        {
            if (array.Exists())
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] == null)
                        return i;
                }

                return -1;
            }

            else
                return -1;
        }



        /// <summary>
        /// Returns true if the array is NOT equal null and Length is greater than 0 (zero)
        /// </summary>
        public static bool Exists<T>(this T[] array, bool debug = false)
        {
            if (array != null && array.Length > 0)
            {
                if (debug)
                    Debug.Log($"Array: <color=white>{array}</color> - <color=green>FOUND</color> ");

                return true;
            }

            else
            {
                if (debug)
                    Debug.Log($"Array: <color=white>{array}</color> - <color=red>NOT FOUND</color> ");

                return false;
            }
        }

        /// <summary>
        /// Returns true if the array is NOT equal null and Length is greater than minLength
        /// </summary>
        public static bool Exists<T>(this T[] array, int minLength, bool debug = false)
        {
            if (array != null && array.Length >= minLength)
            {
                if (debug)
                    Debug.Log($"Array: <color=white>{array}</color> - <color=green>FOUND</color> ");

                return true;
            }

            else
            {
                if (debug)
                    Debug.Log($"Array: <color=white>{array}</color> - <color=red>NOT FOUND</color> ");

                return false;
            }
        }


        /// <summary>
        /// Check weather the array contains the reference object
        /// </summary>
        public static bool ContainsObject<T>(this T[] array, T referenceObject, bool debug = false)
        {
            if (array.Exists())
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].Equals(referenceObject))
                    {
                        if (debug)
                            Debug.Log($"Object: <color=white>{referenceObject}</color>, <color=green>FOUND</color> in array: <color=white>{array}</color>");

                        return true;
                    }
                }

                if (debug)
                    Debug.Log($"Object: <color=white>{referenceObject}</color>, <color=red>NOT FOUND</color> in array: <color=white>{array}</color>");

                return false;
            }

            else
            {
                if (debug)
                    Debug.Log($"Array doesn't exist");

                return false;
            }
        }

        /// <summary>
        /// Find and empty spot on array and fill it with selected element
        /// </summary>
        public static bool Add<T>(this T[] array, T element)
        {
            if (array.Exists())
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] == null)
                    {
                        array[i] = element;
                        return true;
                    }
                }

                return false;
            }

            else
                return false;
        }

        public static void Add<T>(this T[] arrayContainer, List<T> listToAdd)
        {
            for (int i = 0; i < listToAdd.Count; i++)
                arrayContainer.Add(listToAdd[i]);
        }

        public static void Add<T>(this T[] arrayContainer, T[] arrayToAdd)
        {
            for (int i = 0; i < arrayToAdd.Length; i++)
                arrayContainer.Add(arrayToAdd[i]);
        }


        public static int GetRandomIndex<T>(this T[] array)
            => Random.Range(0, array.Length);

        public static T GetRandom<T>(this T[] array)
            => array[Random.Range(0, array.Length)];

        /// <summary>
        /// Returns a single random element from 0 to MaxIndex (inclusive)
        /// </summary>
        public static T GetRandomMax<T>(this T[] array, int maxIndex)
            => array[Random.Range(0, Mathf.Clamp(maxIndex + 1, 0, array.Length))];

        public static T[] GetRandomNonRepeat<T>(this T[] array, int amount)
        {
            amount = Mathf.Clamp(amount, 0, array.Length - 1);
            List<T> randList = new List<T>(array);

            for (int i = 0; i < amount; i++)
            {
                int randomIndex = Random.Range(i, array.Length);
                T temp = randList[i];
                randList[i] = randList[randomIndex];
                randList[randomIndex] = temp;
            }

            return randList.ToArray();
        }

        /// <summary>
        /// Get Random Elemements from array as a list, from "minAmount" to Array full length
        /// </summary>
        public static T[] GetRandomNonRepeatWithMin<T>(this T[] array, int minAmount)
        {
            int amount = Random.Range(minAmount, array.Length);
            return array.GetRandomNonRepeat(amount).ToArray();
        }



        public static T[] RemoveDuplicates<T>(this T[] array)
        {
            List<T> list = new List<T>();
            for (int i = 0; i < array.Length; i++)
                list.Add(array[i]);

            return list.RemoveDuplicates().ToArray();
        }

        public static T[] ReverseArray<T>(this T[] array)
        {
            var reversed = new List<T>();

            for (int i = 0; i < array.Length; i++)
            {
                int index = Mathf.Clamp(array.Length - 1 - i, 0, array.Length);
                reversed.Add(array[index]);
            }

            return reversed.ToArray();
        }


        // Converts an array into a List
        public static List<T> ToList<T>(this T[] array)
        {
            var list = new List<T>();

            for (int i = 0; i < array.Length; i++)
                list.Add(array[i]);

            return list;
        }

        /// <summary>
        /// Returns an Integer confined to array length/bounds
        /// <para>Min = 0, Max = array length - 1</para>
        /// </summary>
        public static int ClampIntToArrayLength<T>(this T[] array, int integer)
            => Mathf.Clamp(integer, 0, array.Length - 1);



        #endregion // arrays



        #region Lists

        public static bool IsIndexWithinBounds<T>(this List<T> list, int index)
        {
            return index >= 0 && index < list.Count;
        }


        /// <summary>
        /// Returns a boolean if the list is NOT == null and Count > than 0 (zero)
        /// </summary>
        public static bool Exists<T>(this List<T> list)
            => list != null && list.Count > 0 ? true : false;

        public static bool AddSafe<T>(this List<T> list, T element)
        {
            if (list != null && list.Contains(element) == false)
            {
                list.Add(element);
                return true;
            }

            else
                return false;
        }

        public static bool RemoveSafe<T>(this List<T> list, T element)
        {
            if (list != null && list.Contains(element))
            {
                list.Remove(element);
                return true;
            }

            else
                return false;
        }

        public static List<T> RemoveEmpty<T>(this List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == null)
                    list.Remove(list[i]);
            }

            return list;
        }


        /// <summary>
        /// Returns the FIRST element inside the list
        /// </summary>
        public static T GetFirst<T>(this List<T> list) => list[0];

        /// <summary>
        /// Returns the LAST element inside the list
        /// </summary>
        public static T GetLast<T>(this List<T> list) => list[list.Count - 1];


        /// <summary>
        /// Returns => list.Count - 1;
        /// </summary>
        public static int GetLastIndex<T>(this List<T> list) => list.Count - 1;

        public static bool IsLastIndex<T>(this List<T> list, int index)
        {
            if (index >= list.GetLastIndex())
                return true;
            else
                return false;
        }

        public static void Add<T>(this List<T> listContainer, List<T> listToAdd)
        {
            for (int i = 0; i < listToAdd.Count; i++)
                listContainer.Add(listToAdd[i]);
        }

        public static void Add<T>(this List<T> listContainer, T[] arrayToAdd)
        {
            for (int i = 0; i < arrayToAdd.Length; i++)
                listContainer.Add(arrayToAdd[i]);
        }



        /// <summary>
        /// Extension method for arrays that returns the index of the next element in a circular manner.
        /// If the input array is null or empty, an error is logged, and an invalid index value (-1) is returned.
        /// </summary>
        public static int GetNextIndex<T>(this List<T> list, int currentIndex)
        {
            if (list == null || list.Count == 0)
            {
                Debug.LogError("Array is null or empty!");
                return -1; // Return an invalid index value
            }

            int nextIndex = (currentIndex + 1) % list.Count;
            return nextIndex;
        }

        public static int GetNextIndexClamped<T>(this List<T> list, int currentIndex)
        {
            if (list == null || list.Count == 0)
            {
                Debug.LogError("Array is null or empty!");
                return -1; // Return an invalid index value
            }

            int nextIndex = Mathf.Clamp(currentIndex + 1, 0, list.Count - 1);
            return nextIndex;
        }


        /// <summary>
        /// Returns a single random element from a list
        /// </summary>
        public static T GetRandom<T>(this List<T> list)
        {
            int randomIndex = Random.Range(0, list.Count);
            return list[randomIndex];
        }

        /// <summary>
        /// Returns a list populated with several random elements from a list without repeating values
        /// </summary>
        public static List<T> GetRandomNonRepeat<T>(this List<T> list, int amount)
        {
            if (list.Count <= amount)
                amount = list.ClampIntToListCount(amount);

            List<T> randList = new List<T>();

            for (int i = 0; i < amount; i++)
            {
                T rand = list.GetRandom();

                while (randList.Contains(rand))
                    rand = list.GetRandom();

                randList.Add(rand);
            }

            return randList;
        }

        public static List<T> ReverseList<T>(this List<T> list, bool debug = false)
        {
            if (list.Count == 0)
            {
                Debug.Log("List too short!");
                return list;
            }

            var reversed = new List<T>();

            for (int i = 0; i < list.Count; i++)
            {
                int index = Mathf.Clamp(list.Count - 1 - i, 0, list.Count - 1);

                if (debug)
                {
                    Debug.Log("List count: " + list.Count);
                    Debug.Log("Reversed Index: " + index);
                }

                reversed.Add(list[index]);
            }

            return reversed;
        }

        /// <summary>
        /// Returns an Integer confined to list count/bounds
        /// <para>Min = 0, Max = list count - 1</para>
        /// </summary>
        public static int ClampIntToListCount<T>(this List<T> list, int integer)
            => Mathf.Clamp(integer, 0, list.Count - 1);

        public static List<T> RemoveDuplicates<T>(this List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                T item = list[i];

                for (int j = 0; j < list.Count; j++)
                {
                    if (j == i)
                        continue;

                    if (item.Equals(list[j]))
                        list.Remove(list[j]);
                }
            }

            return list;
        }

        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }

        #endregion // lists

        #endregion // Arrays & Lists


        #region Children, Transform & Components

        /// <summary>
        /// Returns an Array of all childrens Transforms of parent transform
        /// </summary>
        public static Transform[] GetChildrenArray(this Transform parent)
        {
            Transform[] childrenArray = new Transform[parent.childCount];

            for (int i = 0; i < parent.childCount; i++)
                childrenArray[i] = parent.GetChild(i);

            return childrenArray;
        }


        /// <summary>
        /// Returns a List of all childrens Transforms of parent transform
        /// </summary>
        public static List<Transform> GetChildrenList(this Transform parent)
        {
            List<Transform> childrenList = new List<Transform>();

            for (int i = 0; i < parent.childCount; i++)
                childrenList.Add(parent.GetChild(i));

            return childrenList;
        }


        /// <summary>
        /// Returns a list of all childrens GameObjects of parent transform
        /// </summary>
        public static List<GameObject> GetChildrenGameObjects(this Transform parent)
        {
            List<GameObject> childrenList = new List<GameObject>();

            for (int i = 0; i < parent.childCount; i++)
                childrenList.Add(parent.GetChild(i).gameObject);

            return childrenList;
        }


        /// <summary>
        /// Returns a List of components found ONLY under the parent transform. Doesn't search bellow the Transform parent 
        /// </summary>
        public static List<T> GetComponentListInChildren<T>(this Transform parent) //where T : MonoBehaviour
        {
            if (parent != null && parent.childCount > 0)
            {
                List<T> components = new List<T>();

                for (int i = 0; i < parent.childCount; i++)
                {
                    Transform child = parent.GetChild(i);
                    T component = child.GetComponent<T>();
                    if (component != null)
                        components.Add(component);
                }

                return components;
            }

            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns an Array of components found ONLY under the parent transform. Doesn't search bellow the Transform parent 
        /// </summary>
        public static T[] GetComponentArrayInChildren<T>(this Transform parent)
        {
            if (parent != null)
            {
                T[] componentArray = parent.GetComponentListInChildren<T>()?.ToArray();

                if (componentArray != null)
                    return componentArray;
                else
                    return null;
            }

            else
            {
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



        public static void SetGameObjectsActive(GameObject[] gameObjects, bool value)
        {
            for (int i = 0; i < gameObjects.Length; i++)
                gameObjects[i].SetActive(value);
        }


        public static void SetGameObjectsActive(List<GameObject> gameObjects, bool value)
        {
            for (int i = 0; i < gameObjects.Count; i++)
                gameObjects[i].SetActive(value);
        }
        #endregion // Children, Transform & Components


        #region Effects

        #region Lines
        /// <summary>
        /// Set a single Width to line
        /// </summary>
        public static void SetLineWidth(this LineRenderer line, float width)
        {
            line.startWidth = width;
            line.endWidth = width;
        }


        /// <summary>
        /// Sets line point count and points from an array
        /// </summary>
        public static void SetLinePoints(this LineRenderer line, Vector3[] points)
        {
            line.positionCount = points.Length;
            line.SetPositions(points);
        }

        /// <summary>
        /// Sets line point count and points from a list
        /// </summary>
        public static void SetLinePoints(this LineRenderer line, List<Vector3> points)
        {
            line.positionCount = points.Count;
            line.SetPositions(points.ToArray());
        }


        /// <summary>
        /// Sets line point count and points from an array
        /// </summary>
        public static void SetLinePointsWithHeightOffset(this LineRenderer line, Vector3[] points, float heightOffset)
        {
            line.positionCount = points.Length;
            for (int i = 0; i < points.Length; i++)
                points[i].y = heightOffset;

            line.SetPositions(points);
        }


        /// <summary>
        /// Sets 2 points in a line
        /// </summary>
        public static void SetTwoPointLine(this LineRenderer line, Vector3 startPoint, Vector3 endPoint)
        {
            line.positionCount = 2;
            line.SetPosition(0, startPoint);
            line.SetPosition(1, endPoint);
        }


        /// <summary>
        /// Sets a color to Line start and end
        /// </summary>
        public static void SetLineColor(this LineRenderer line, Color color)
        {
            line.startColor = color;
            line.endColor = color;
        }


        /// <summary>
        /// Sets a color to Line start and end
        /// </summary>
        public static void SetLineOpacity(this LineRenderer line, float opacity)
        {
            Color colorStart = line.startColor;
            Color colorEnd = line.endColor;

            colorStart.a = opacity;
            colorEnd.a = opacity;

            line.startColor = colorStart;
            line.endColor = colorEnd;
        }


        /// <summary>
        /// Clears all line points
        /// </summary>
        public static void ClearLinePoints(this LineRenderer line)
        {
            line.positionCount = 0;
            line.SetPositions(new Vector3[0]);
        }

        #endregion // Line


        #region Trails



        /// <summary>
        /// Sets a color to Trail start and end
        /// </summary>
        public static void SetTrailColor(this TrailRenderer trail, Color color)
        {
            trail.startColor = color;
            trail.endColor = color;
        }


        /// <summary>
        /// Sets a color to Line start and end
        /// </summary>
        public static void SetTrailOpacity(this TrailRenderer trail, float opacity)
        {
            Color colorStart = trail.startColor;
            Color colorEnd = trail.endColor;

            colorStart.a = opacity;
            colorEnd.a = opacity;

            trail.startColor = colorStart;
            trail.endColor = colorEnd;
        }



        /// <summary>
        /// Removes all points from each Trail Renderer
        /// </summary>
        public static void ClearTrails(TrailRenderer[] trailRenderers)
        {
            if (trailRenderers.Exists() == false)
                return;

            for (int i = 0; i < trailRenderers.Length; i++)
                trailRenderers[i].Clear();
        }

        /// <summary>
        /// Removes all points from each Trail Renderer
        /// </summary>
        public static void ClearTrails(List<TrailRenderer> trailRenderers)
        {
            if (trailRenderers.Exists() == false)
                return;

            for (int i = 0; i < trailRenderers.Count; i++)
                trailRenderers[i].Clear();
        }



        /// <summary>
        /// Enabled each Trail Renderer
        /// </summary>
        public static void PlayTrails(TrailRenderer[] trailRenderers)
        {
            if (trailRenderers.Exists() == false)
                return;

            for (int i = 0; i < trailRenderers.Length; i++)
                trailRenderers[i].enabled = true;
        }
        /// <summary>
        /// Enables each Trail Renderer
        /// </summary>
        public static void PlayTrails(List<TrailRenderer> trailRenderers)
        {
            if (trailRenderers.Exists() == false)
                return;

            for (int i = 0; i < trailRenderers.Count; i++)
                trailRenderers[i].enabled = true;
        }



        /// <summary>
        /// Disables each Trail Renderer
        /// </summary>
        public static void StopTrails(TrailRenderer[] trailRenderers)
        {
            if (trailRenderers.Exists() == false)
                return;

            for (int i = 0; i < trailRenderers.Length; i++)
                trailRenderers[i].enabled = false;
        }

        /// <summary>
        /// Disables each Trail Renderer
        /// </summary>
        public static void StopTrails(List<TrailRenderer> trailRenderers)
        {
            if (trailRenderers.Exists() == false)
                return;

            for (int i = 0; i < trailRenderers.Count; i++)
                trailRenderers[i].enabled = false;
        }

        #endregion // Trails


        #region Particles

        /// <summary>
        /// Removes all particles from each Particle System
        /// </summary>
        public static void ClearParticles(ParticleSystem[] particleSystems)
        {
            if (particleSystems.Exists() == false)
                return;

            for (int i = 0; i < particleSystems.Length; i++)
                particleSystems[i].Clear();
        }

        /// <summary>
        /// Removes all particles from each Particle System
        /// </summary>
        public static void ClearParticles(List<ParticleSystem> particleSystems)
        {
            if (particleSystems.Exists() == false)
                return;

            for (int i = 0; i < particleSystems.Count; i++)
                particleSystems[i].Clear();
        }

        /// <summary>
        /// Starts the particles for each Particle System
        /// </summary>
        public static void PlayParticles(ParticleSystem[] particleSystems)
        {
            if (particleSystems.Exists() == false)
                return;

            for (int i = 0; i < particleSystems.Length; i++)
                particleSystems[i].Play();
        }

        /// <summary>
        /// Starts the particles for each Particle System
        /// </summary>
        public static void PlayParticles(List<ParticleSystem> particleSystems)
        {
            if (particleSystems.Exists() == false)
                return;

            for (int i = 0; i < particleSystems.Count; i++)
                particleSystems[i].Play();
        }

        /// <summary>
        /// Stops the particles for each Particle System
        /// </summary>
        public static void StopParticles(ParticleSystem[] particleSystems)
        {
            if (particleSystems.Exists() == false)
                return;

            for (int i = 0; i < particleSystems.Length; i++)
                particleSystems[i].Stop();
        }

        /// <summary>
        /// Stops the particles for each Particle System
        /// </summary>
        public static void StopParticles(List<ParticleSystem> particleSystems)
        {
            if (particleSystems.Exists() == false)
                return;

            for (int i = 0; i < particleSystems.Count; i++)
                particleSystems[i].Stop();
        }
        #endregion // Particles

        #endregion // Effects


        #region Images

        public static void SetImageAlpha(UnityEngine.UI.Image image, float alpha)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }

        public static void SetImageArrayColor(UnityEngine.UI.Image[] images, Color color)
        {
            for (int i = 0; i < images.Length; i++)
                images[i].color = color;
        }

        public static void SetImageArrayColor(List<UnityEngine.UI.Image> images, Color color)
        {
            for (int i = 0; i < images.Count; i++)
                images[i].color = color;
        }

        public static void SetImageArrayAlpha(UnityEngine.UI.Image[] images, float alpha)
        {
            Color color = Color.black;

            for (int i = 0; i < images.Length; i++)
            {
                color = images[i].color;
                color.a = alpha;

                images[i].color = color;
            }
        }

        public static void SetImageArrayAlpha(List<UnityEngine.UI.Image> images, float alpha)
        {
            Color color = Color.black;

            for (int i = 0; i < images.Count; i++)
            {
                color = images[i].color;
                color.a = alpha;

                images[i].color = color;
            }
        }

        public static void SetImagesPixelsPerUnit(UnityEngine.UI.Image[] images, float pixelsPerUnit)
        {
            for (int i = 0; i < images.Length; i++)
                images[i].pixelsPerUnitMultiplier = pixelsPerUnit;
        }

        public static void SetImagesPixelsPerUnit(List<UnityEngine.UI.Image> images, float pixelsPerUnit)
        {
            for (int i = 0; i < images.Count; i++)
                images[i].pixelsPerUnitMultiplier = pixelsPerUnit;
        }

        public static void SetTextMeshArrayColor(TMPro.TextMeshProUGUI[] textMeshes, Color color)
        {
            for (int i = 0; i < textMeshes.Length; i++)
                textMeshes[i].color = color;
        }

        #endregion // Images


        #region Sprite Renderer

        public static void SetSpriteRendererAlpha(SpriteRenderer spriteRenderer, float alpha)
        {
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;
        }

        public static void SetSpriteRendererArrayAlpha(SpriteRenderer[] spriteRenderers, float alpha)
        {
            if (spriteRenderers.Exists() == false)
                return;

            for (int i = 0; i < spriteRenderers.Length; i++)
                SetSpriteRendererAlpha(spriteRenderers[i], alpha);
        }

        public static void SetSpriteRendererArrayColor(SpriteRenderer[] spriteRenderers, Color color)
        {
            if (spriteRenderers.Exists() == false)
                return;

            for (int i = 0; i < spriteRenderers.Length; i++)
                spriteRenderers[i].color = color;
        }

        #endregion // Sprite Renderer


        #region Text Files

        /// <summary>
        /// Puts the string into the Clipboard.
        /// </summary>
        public static void CopyStringToClipboard(this string str)
        {
            GUIUtility.systemCopyBuffer = str;
        }

        /// <summary>
        /// Creates new file if there isn't one or adds contents to an existing one, and Returns its path
        /// File name ex: 'logs.txt' (extensions can be whatever ex: .bnb, .cro, etc,.)
        /// </summary>
        public static string CreateOrAddTextToFile(string path, string fileName, string content, int newLineAmount = 0)
        {
            // path to file
            string filePath = path + fileName;

            // add new lines
            if (newLineAmount > 0)
                for (int i = 0; i < newLineAmount; i++)
                    content += "\n";

            // create file it if doesnt exist
            if (File.Exists(filePath) == false)
                File.WriteAllText(filePath, content);

            // add content to file
            else
                File.AppendAllText(filePath, content);

            return filePath;
        }

        /// <summary>
        /// Creates new file if there isn't one or adds contents to an existing one, and Returns its path
        /// File name ex: 'logs.txt' (extensions can be whatever ex: .bnb, .cro, etc,.)
        /// </summary>
        public static string CreateOrAddTextToFile(string path, string content)
        {
            // create file it if doesnt exist
            if (File.Exists(path) == false)
                File.WriteAllText(path, content);

            // add content to file
            else
                File.AppendAllText(path, content);

            return path;
        }

        public static string[] GetLinesFromTextAsset(TextAsset textAsset, bool removeEmtpyLines = true)
        {
            if (textAsset == null)
            {
                Debug.LogError("TextAsset is null!");
                return null;
            }

            // Split the text asset content by new lines
            string[] lines = textAsset.text.Split('\n');

            for (int i = 0; i < lines.Length; i++)
                lines[i] = lines[i].Trim();

            return lines;
        }

        public static string[] GetNonEmptyLinesFromTextAsset(TextAsset textAsset)
        {
            if (textAsset == null)
            {
                Debug.LogError("TextAsset is null!");
                return null;
            }

            // Split the text asset content by new lines
            string[] lines = textAsset.text.Split('\n');
            List<string> result = new List<string>();

            for (int i = 0; i < lines.Length; i++)
            {
                string trimmedLine = lines[i].Trim();

                if (string.IsNullOrEmpty(trimmedLine) == false || trimmedLine.Length > 0)
                    result.Add(trimmedLine);
            }

            return result.ToArray();
        }

        #endregion //  Text Files


        #region Raycast & Collision

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

        #endregion // Raycast & Collision


        #region Color

        /// <summary>
        /// 0-transparent, 1-opaque
        /// </summary>
        public static Color ChangeColorOpacity(Color color, float opacity)
        {
            color.a = opacity;
            return color;
        }

        /// <summary>
        /// 0-transparent, 1-opaque
        /// </summary>
        public static Color[] ChangeColorsOpacity(Color[] colors, float opacity)
        {
            Color[] opaqueColors = new Color[colors.Length];
            for (int i = 0; i < colors.Length; i++)
            {
                Color color = colors[i];
                opaqueColors[i] = ChangeColorOpacity(color, opacity);
            }

            return opaqueColors;
        }

        /// <summary>
        /// 0-transparent, 1-opaque
        /// </summary>
        public static List<Color> ChangeColorsOpacity(List<Color> colors, float opacity)
        {
            List<Color> opaqueColors = new List<Color>();
            for (int i = 0; i < colors.Count; i++)
            {
                Color color = colors[i];
                opaqueColors.Add(ChangeColorOpacity(color, opacity));
            }

            return opaqueColors;
        }

        public static Color[] GetColorsOpaque(Color[] colors)
        {
            return ChangeColorsOpacity(colors, 1);
        }

        public static List<Color> GetColorsOpaque(List<Color> colors)
        {
            return ChangeColorsOpacity(colors, 1);
        }


        /// <summary>
        /// Given an array of colors, returns each color and a lerped value between them
        /// </summary>
        public static Color[] GenerateLerpedColors(Color[] inputColors)
        {
            if (inputColors == null || inputColors.Length < 2)
            {
                Debug.LogError("Invalid input. The array of colors must have at least two elements, and the color count must be at least two.");
                return null;
            }

            List<Color> colors = new List<Color>();
            for (int i = 0; i < inputColors.Length; i++)
            {
                int colorIndex = i;
                int nextColorIndex = i + 1;
                colors.Add(inputColors[i]);

                if (i < inputColors.Length - 1)
                {
                    colors.Add(Color.Lerp(inputColors[colorIndex], inputColors[nextColorIndex], .5f));
                }
            }

            return colors.ToArray();
        }

        #endregion // Color


        #region Unity Editor
#if UNITY_EDITOR

        #region Find Objects & ScriptableObjects

        /// <summary>
        /// Given a folder name, returns all Objects of Type T, found inside that folder
        /// </summary>
        public static List<T> FindObjectTypesInFolderOfName<T>(string folderName) where T : Object
        {
            string folderNameToSearch = "Mini Gradients";

            // Search for the folder by its name
            string[] folderGUIDs = AssetDatabase.FindAssets(folderNameToSearch + " t:folder");

            if (folderGUIDs.Length > 0)
            {
                string folderPath = AssetDatabase.GUIDToAssetPath(folderGUIDs[0]);
                Debug.Log("Folder found at path: " + folderPath);

                // Get all asset GUIDs in the specified folder
                string[] assetGUIDs = AssetDatabase.FindAssets("", new[] { folderPath });

                List<T> items = new List<T>();

                foreach (string assetGUID in assetGUIDs)
                {
                    string assetPath = AssetDatabase.GUIDToAssetPath(assetGUID);
                    T item = AssetDatabase.LoadAssetAtPath<T>(assetPath);

                    if (item != null)
                        items.Add(item);
                }

                if (items.Count > 0)
                {
                    Debug.Log("Found " + items.Count + " items in the folder:");

                    foreach (T item in items)
                        Debug.Log("Item: " + item.name);

                    return items;
                }

                else
                    Debug.Log("No items found in the folder.");
            }

            else
                Debug.Log("Folder not found.");

            return null;
        }

        /// <summary>
        /// Given a folder path, returns all Objects of Type T found inside that folder
        /// </summary>
        public static List<T> FindObjectTypesInFolderPath<T>(string folderPath, bool debugName = true) where T : Object
        {
            // Get all asset GUIDs in the specified folder
            string[] assetGUIDs = AssetDatabase.FindAssets("t:" + typeof(T).Name, new[] { folderPath });

            List<T> items = new List<T>();

            foreach (string assetGUID in assetGUIDs)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(assetGUID);
                T item = AssetDatabase.LoadAssetAtPath<T>(assetPath);

                if (item != null)
                    items.Add(item);
            }

            if (items.Count > 0)
            {
                if (debugName == true)
                {
                    Debug.Log("Found " + items.Count + " items in the folder:");

                    foreach (T item in items)
                        Debug.Log("Item: " + item.name);
                }

                return items;
            }

            else
            {
                Debug.Log("No items found in the folder.");
                return null;
            }
        }


        public static T FindScriptableObjectOfTypeInFolderName<T>(string folderName, bool debugNames = false) where T : ScriptableObject
            => FindScriptableObjectsOfTypeInFolderName<T>(folderName, debugNames).GetFirst();

        /// <summary>
        /// Given a folder name, returns all Scriptable Objects of Type T, found inside that folder
        /// </summary>
        public static List<T> FindScriptableObjectsOfTypeInFolderName<T>(string folderName, bool debugNames = false) where T : ScriptableObject
        {
            // Search for the folder by its name
            string[] folderGUIDs = AssetDatabase.FindAssets(folderName + " t:folder");

            if (folderGUIDs.Length > 0)
            {
                string folderPath = AssetDatabase.GUIDToAssetPath(folderGUIDs[0]);
                Debug.Log("Folder found at path: " + folderPath);

                // Get all asset GUIDs in the specified folder
                string[] assetGUIDs = AssetDatabase.FindAssets($"t:{typeof(T)}", new[] { folderPath });

                List<T> items = new List<T>();

                foreach (string assetGUID in assetGUIDs)
                {
                    string assetPath = AssetDatabase.GUIDToAssetPath(assetGUID);
                    T item = AssetDatabase.LoadAssetAtPath<T>(assetPath);

                    if (item != null)
                        items.Add(item);
                }

                if (items.Count > 0)
                {
                    if (debugNames)
                    {
                        Debug.Log("Found " + items.Count + " items in the folder:");

                        foreach (T item in items)
                            Debug.Log("Item: " + item.name);
                    }

                    return items;
                }

                else
                {
                    Debug.Log("No items found in the folder.");
                }
            }

            else
            {
                Debug.Log("Folder not found.");
            }

            return null;
        }

        /// <summary>
        /// Given a folder path, returns all Scriptable Objects of Type T, found inside that folder
        /// </summary>
        public static List<T> FindScriptableObjectTypesInFolderPath<T>(string folderPath, bool debugNames = true) where T : ScriptableObject
        {
            // Get all asset GUIDs in the specified folder
            string[] assetGUIDs = AssetDatabase.FindAssets($"t:{typeof(T)}", new[] { folderPath });

            List<T> items = new List<T>();

            foreach (string assetGUID in assetGUIDs)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(assetGUID);
                T item = AssetDatabase.LoadAssetAtPath<T>(assetPath);

                if (item != null)
                    items.Add(item);
            }

            if (items.Count > 0)
            {
                if (debugNames)
                {
                    Debug.Log("Found " + items.Count + " items in the folder:");

                    foreach (T item in items)
                        Debug.Log("Item: " + item.name);
                }

                return items;
            }

            else
            {
                Debug.Log("No items found in the folder.");
            }

            return null;
        }

        #endregion // Find Objects & ScriptableObjects


        #region Move Assets

        /// <summary>
        /// Moves the whole contents of a folder to another folder, given both folder paths
        /// </summary>
        public static void MoveFolderContentsToAnotherFolderPath(string sourceFolderPath, string destinationFolderPath)
        {
            // Get all asset paths in the source folder
            string[] assetPaths = AssetDatabase.FindAssets("", new[] { sourceFolderPath });

            foreach (var assetPath in assetPaths)
            {
                string assetFullPath = AssetDatabase.GUIDToAssetPath(assetPath);

                // Create the destination path by replacing the source folder path with the destination folder path
                string destinationFullPath = assetFullPath.Replace(sourceFolderPath, destinationFolderPath);

                // Ensure the destination folder exists, create it if not
                string destinationFolder = Path.GetDirectoryName(destinationFullPath);
                if (!Directory.Exists(destinationFolder))
                {
                    Directory.CreateDirectory(destinationFolder);
                }

                // Move the asset
                AssetDatabase.MoveAsset(assetFullPath, destinationFullPath);
            }

            AssetDatabase.Refresh(); // Refresh the Asset Database to reflect the changes in the Editor
            Debug.Log("Assets moved successfully!");
        }


        /// <summary>
        /// Moves the reference asset to a destination folder, given the folder path
        /// </summary>
        public static void MoveAssetToPath(Object asset, string destinationFolderPath)
        {
            if (asset == null)
            {
                Debug.LogError("Asset is null!");
                return;
            }

            // Get the asset path
            string assetPath = AssetDatabase.GetAssetPath(asset);

            // Ensure the asset path is not empty
            if (string.IsNullOrEmpty(assetPath))
            {
                Debug.LogError("Invalid asset path!");
                return;
            }

            // Create the destination path by combining the destination folder and the asset's name
            string assetName = Path.GetFileName(assetPath);
            string destinationFullPath = Path.Combine(destinationFolderPath, assetName);

            // Ensure the destination folder exists, create it if not
            string destinationFolder = Path.GetDirectoryName(destinationFullPath);
            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            // Move the asset
            AssetDatabase.MoveAsset(assetPath, destinationFullPath);

            AssetDatabase.Refresh(); // Refresh the Asset Database to reflect the changes in the Editor
            Debug.Log("Asset moved successfully to: " + destinationFullPath);
        }

        #endregion // Move Assets


        #region Instantiate Prefabs
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
        public static ScriptableObject CreateScriptableObject(string scriptableClassName, string directoryPath, string name)
        {
            // Make sure the directory exists, create it if not
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string assetPath = Path.Combine(directoryPath, name + ".asset");

            ScriptableObject so = ScriptableObject.CreateInstance(scriptableClassName);
            so.name = name;

            AssetDatabase.CreateAsset(so, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh(); // Refresh the asset database to reflect changes in the Editor
            Selection.activeObject = so;

            return so;
        }

        public static T CreatePrefab<T>(GameObject prefab, Transform parent)
        {
            var obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            obj.transform.parent = parent;
            return obj.GetComponent<T>();
        }

        public static void DestroyAsset(Object asset)
        {
            if (asset == null)
            {
                Debug.LogError("Asset is null!");
                return;
            }

            string assetPath = AssetDatabase.GetAssetPath(asset);

            if (!string.IsNullOrEmpty(assetPath))
            {
                AssetDatabase.DeleteAsset(assetPath);
                Debug.Log("Asset deleted: " + assetPath);
            }

            else
            {
                Debug.LogError("Invalid asset path!");
            }
        }

        #endregion // instantiate prefabs


        #region Gizmos

        public static void DrawString(Color color, string text, Vector3 position)
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = color;

            Handles.Label(position, text, style);
        }

        public static void DrawPoint(Color color, Vector3 position, float radius)
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(position, radius);
        }

        public static void DrawLine(Color color, Vector3 origin, Vector3 target)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(origin, target);
        }

        public static void DrawRay(Color color, Vector3 origin, Vector3 direction)
        {
            Gizmos.color = color;
            Gizmos.DrawRay(origin, direction);
        }

        public static void DrawCube(Color color, Vector3 position, Vector3 scale, Quaternion rotation)
        {
            Gizmos.color = color;

            Matrix4x4 cubeTransform = Matrix4x4.TRS(position, rotation, scale);
            Matrix4x4 oldGizmosMatrix = Gizmos.matrix;

            Gizmos.matrix *= cubeTransform;
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
            Gizmos.matrix = oldGizmosMatrix;
        }

        public static void DrawArrow(Color color, Vector3 arrowPosition, Vector3 arrowDirection, float arrowLength = 1f, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20f)
        {
            Gizmos.color = color;
            Vector3 arrowEnd = arrowPosition + arrowDirection.normalized * arrowLength;

            // Draw arrow body
            Gizmos.DrawLine(arrowPosition, arrowEnd);

            // Draw arrow head
            Vector3 right = Quaternion.LookRotation(arrowDirection) * Quaternion.Euler(0, -arrowHeadAngle, 0) * Vector3.forward;
            Vector3 left = Quaternion.LookRotation(arrowDirection) * Quaternion.Euler(0, arrowHeadAngle, 0) * Vector3.forward;

            Gizmos.DrawLine(arrowEnd, arrowEnd - right * arrowHeadLength);
            Gizmos.DrawLine(arrowEnd, arrowEnd - left * arrowHeadLength);
        }
        #endregion // gizmos

#endif
        #endregion // Unity Editor
    }
}