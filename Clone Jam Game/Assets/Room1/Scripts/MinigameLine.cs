using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameLine : InteractableObject
{
    public override void Interact()
    {
        SceneManager.LoadScene("EmirMinigameScene", LoadSceneMode.Additive);

        Player.Instance.enabled = false;
    }
}
