using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToilateDoor : InteractableObject
{
    public static event Action PlayerOnTheToilate;
    public static event Action PlayerExitTheToilate;

    public GameObject Arrow;
    public GameObject Player;
    private bool currentlyInteracting = false;
    public override void Interact()
    {
        if (!currentlyInteracting)
        {
            Player.SetActive(false);
            Arrow.SetActive(true);
            currentlyInteracting = true;
            PlayerOnTheToilate?.Invoke();
        }
        else {
            Player.SetActive(true);
            Arrow.SetActive(false);
            currentlyInteracting = false;
            PlayerExitTheToilate?.Invoke();
        }
    }

}
