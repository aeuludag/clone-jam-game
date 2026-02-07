using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : InteractableObject
{
    public string roomToTransform;
    public string spawnPointName;
    public override void Interact()
    {
        if (roomToTransform != null && spawnPointName != null) {
            SceneTransitionManager.TargetSpawnName = spawnPointName;
            SceneManager.LoadScene(roomToTransform);
        }
    }
}
