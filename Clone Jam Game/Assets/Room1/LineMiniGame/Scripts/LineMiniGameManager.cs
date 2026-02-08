using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public HandController hand;
    public Button endWorkButton;
    public TextMeshProUGUI countTMP;
    public int threshold;


    // Update is called once per frame (comment to shut errors)
    void Update()
    {
        endWorkButton.interactable = hand.points >= threshold;
        countTMP.text = hand.points.ToString();
    }

    public void CloseMinigame(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);

        if (Player.Instance != null)
        {
            Player.Instance.enabled = true;
        }
    }
}
