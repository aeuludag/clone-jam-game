using System.Collections.Generic;
using UnityEngine;

public class DayCycleManager : MonoBehaviour
{

    public static DayCycleManager Instance;

    public string[] openedDoors;

    public int currentDay = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsDoorOpen(string doorID) {
        for (int i = 0; i < openedDoors.Length; i++) {
            if (openedDoors[i] == doorID) { 
                return true;
            }
        }
        return false;
    }

    public void MarkDoorAsOpen(string doorID)
    {
        if (!IsDoorOpen(doorID))
        {
            openedDoors[openedDoors.Length] = (doorID);
        }
    }
}
