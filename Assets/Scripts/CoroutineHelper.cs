using Unity.VisualScripting;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    private static CoroutineRunner _instance;
    public static CoroutineRunner Instance
    {
        get
        {
            if (_instance == null)
            {
                var obj = new GameObject("CoroutineRunner");
                DontDestroyOnLoad(obj);
                _instance = obj.AddComponent<CoroutineRunner>();
            }
            return _instance;
        }
    }
}
