using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : InteractableObject
{
    public string doorId;
    public string roomToTransform;
    public string spawnPointName;

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
        if (!isLocked)
        {
            if (roomToTransform != null && spawnPointName != null)
            {
                SceneTransitionManager.TargetSpawnName = spawnPointName;
                SceneManager.LoadScene(roomToTransform);
            }
        }
        else { 
            // TODO: Kapý kilitili uayrýsý lazým!
        }
    }

    public void UnlockDoor()
    {
        isLocked = false;
        DayCycleManager.Instance.MarkDoorAsOpen(doorId);
        Debug.Log($"Door {doorId} is now permanently unlocked!");
    }
}
