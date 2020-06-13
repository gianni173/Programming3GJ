using UnityEngine;
using Sirenix.OdinInspector;

public abstract class SingletonScriptable <T>: ScriptableObject where T: ScriptableObject {

    //Properties
    private static T instance;
    public static T Instance {
        get {
            if (!instance) {
                T[] all = Resources.FindObjectsOfTypeAll<T>();
                instance = (all.Length > 0) ? all[0] : null;
            }

#if UNITY_EDITOR
            if (!instance) {
                string[] configsGUIDs = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(T).Name);
                if (configsGUIDs.Length > 0) {
                    instance = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(UnityEditor.AssetDatabase.GUIDToAssetPath(configsGUIDs[0]));
                }
            }
#endif
            return instance;
        }
    }
}

public abstract class SerializedSingletonScriptable<T> : SerializedScriptableObject where T : SerializedScriptableObject
{

    //Properties
    private static T instance;
    public static T Instance
    {
        get
        {
            if (!instance)
            {
                T[] all = Resources.FindObjectsOfTypeAll<T>();
                instance = (all.Length > 0) ? all[0] : null;
            }

#if UNITY_EDITOR
            if (!instance)
            {
                string[] configsGUIDs = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(T).Name);
                if (configsGUIDs.Length > 0)
                {
                    instance = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(UnityEditor.AssetDatabase.GUIDToAssetPath(configsGUIDs[0]));
                }
            }
#endif
            return instance;
        }
    }
}

