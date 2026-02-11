using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameLine : InteractableObject
{
    public bool hasBeenOpened;
    public override void Interact()
    {
        DayCycleManager.Instance.LineMiniGameClosed = true;
        DayCycleManager.Instance.MarkDoorAsOpen("Room1Right");
        if(hasBeenOpened) return;
        SceneManager.LoadScene("EmirMinigameScene", LoadSceneMode.Additive);
        hasBeenOpened = true;
        Player.Instance.enabled = false;
    }
}
