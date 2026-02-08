using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameLine : InteractableObject
{
    public DayCycleManager DayCycleManager;
    public bool hasBeenOpened;
    public override void Interact()
    {
        DayCycleManager.LineMiniGameClosed = true;
        if(hasBeenOpened) return;
        SceneManager.LoadScene("EmirMinigameScene", LoadSceneMode.Additive);
        hasBeenOpened = true;
        Player.Instance.enabled = false;
    }
}
