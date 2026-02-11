using UnityEngine;
using UnityEngine.SceneManagement;

public class NumpaddedDoor : MonoBehaviour
{
    public LinePuzzleManager linePuzzleManager;
    public DialogueBox dialogueBox;
    public Dialogue dialogue;
    private Door door;
    void Start()
    {
        door = GetComponent<Door>();
    }

    public void OpenNumpad()
    {
        if(!linePuzzleManager.done)
        {
            dialogueBox.StartDialogue(dialogue);
            return;
        }
        if (DayCycleManager.Instance.isNumpaddedDoorUnlocked)
        {
            DayCycleManager.Instance.MarkDoorAsOpen(door.doorId);
            door.Interact();
        }
        else
        {
            SceneManager.LoadScene("KeypadDoor", LoadSceneMode.Additive);
            Player.Instance.gameObject.SetActive(false);
        }
    }

}
