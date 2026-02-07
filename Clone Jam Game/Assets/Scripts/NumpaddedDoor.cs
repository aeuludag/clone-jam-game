using UnityEngine;
using UnityEngine.SceneManagement;

public class NumpaddedDoor : MonoBehaviour
{
    private Door door;
    void Start()
    {
        door = GetComponent<Door>();
    }

    public void OpenNumpad()
    {
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
