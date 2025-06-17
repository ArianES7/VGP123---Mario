using UnityEngine;

public class Singelton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Only this class (and its subclasses) can set Instance
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
            // Persist across scenes (optional for GameManager)
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            // There's already one — kill this duplicate
            Destroy(gameObject);
        }
    }
}
