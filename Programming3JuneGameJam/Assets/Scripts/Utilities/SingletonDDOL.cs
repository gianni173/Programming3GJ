using UnityEngine;
using Sirenix.OdinInspector;

public abstract class SingletonDDOL<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;

    public virtual void Awake()
    {
        if (!instance)
        {
            instance = this as T;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

public abstract class SerializedSingletonDDOL<T> : SerializedMonoBehaviour where T : SerializedMonoBehaviour
{
    public static T instance;

    public virtual void Awake()
    {
        if (!instance)
        {
            instance = this as T;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}