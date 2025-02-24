using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    public static T instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
