using System.Collections.Generic; // This line is needed for HashSet<>
using UnityEngine;

public class NoDestroy : MonoBehaviour
{
    private static HashSet<string> persistentObjects = new HashSet<string>();

    void Awake()
    {
        // Check if an object with this name has already been marked to persist
        if (!persistentObjects.Contains(gameObject.name))
        {
            DontDestroyOnLoad(gameObject);
            persistentObjects.Add(gameObject.name);
            Debug.Log(gameObject.name + " set to persist across scenes.");
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates that might arise from reloading the same scene.
            Debug.Log(gameObject.name + " duplicate detected and destroyed.");
        }
    }

    public static void ClearPersistentObjects()
    {
        persistentObjects.Clear(); // Call this when you want to reset the game state completely
    }
}
