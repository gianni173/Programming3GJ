using UnityEngine;
using Sirenix.OdinInspector;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance;

    public virtual void Awake()
    {
        if (!Instance)
        {
            Instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

public abstract class SerializedSingleton<T> : SerializedMonoBehaviour where T : SerializedMonoBehaviour
{
    public static T instance;

    public virtual void Awake()
    {
        if (!instance)
        {
            instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}