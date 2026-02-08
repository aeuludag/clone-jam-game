using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : InteractableObject
{
    public string doorId;
    public string roomToTransform;
    public string spawnPointName;

    public DialogueBox dialogueBox;
    public Dialogue dialogue;

    private bool isLocked = true;

    private void Start()
    {
        if (DayCycleManager.Instance != null && DayCycleManager.Instance.IsDoorOpen(doorId))
        {
            isLocked = false;
        }
    }

    public override void Interact()
    {
        if (DayCycleManager.Instance != null && DayCycleManager.Instance.IsDoorOpen(doorId))
        {
            isLocked = false;
        }

        if (!isLocked)
        {
            if (roomToTransform != null && spawnPointName != null)
            {
                SceneTransitionManager.TargetSpawnName = spawnPointName;
                SceneManager.LoadScene(roomToTransform);
            }
        }
        else {
            if(dialogueBox != null) dialogueBox.StartDialogue(dialogue);
            unityEvent?.Invoke();
        }
    }

    public void UnlockDoor()
    {
        isLocked = false;
        DayCycleManager.Instance.MarkDoorAsOpen(doorId);
    }
}
