using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionHandler : MonoBehaviour
{
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public static void CloseMinigame(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);

        if (Player.Instance != null)
        {
            Player.Instance.enabled = true;
        }
    }
}
