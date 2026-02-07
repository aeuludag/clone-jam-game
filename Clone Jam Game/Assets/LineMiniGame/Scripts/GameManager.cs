using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) { 
            CloseMinigame();
        }
    }

    public void CloseMinigame()
    {
        SceneManager.UnloadSceneAsync("EmirMinigameScene");

        if (Player.Instance != null)
        {
            Player.Instance.enabled = true;
        }
    }
}
