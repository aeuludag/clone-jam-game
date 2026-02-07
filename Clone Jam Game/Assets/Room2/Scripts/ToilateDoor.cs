using UnityEngine;
using UnityEngine.SceneManagement;

public class ToilateDoor : InteractableObject
{
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
        }
        else {
            Player.SetActive(true);
            Arrow.SetActive(false);
            currentlyInteracting = false;
        }
    }

    //private void Update()
    //{
    //    if (currentlyInteracting && Input.GetKeyDown(KeyCode.E)) {
    //        Player.SetActive(true);
    //        Arrow.SetActive(false);
    //        currentlyInteracting = false;
    //    }
    //}
}
