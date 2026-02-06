using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionHandler : MonoBehaviour
{
    public static TransitionHandler Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else if(Instance != this)
        {
            Destroy(Instance);
        }

        DontDestroyOnLoad(Instance);
    }

    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
