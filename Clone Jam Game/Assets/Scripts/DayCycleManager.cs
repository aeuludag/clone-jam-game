using System.Collections.Generic;
using UnityEngine;

public class DayCycleManager : MonoBehaviour
{
    public static DayCycleManager Instance;

    public List<string> openedDoors = new List<string>();

    public int currentDay = 1;
    public bool isNumpaddedDoorUnlocked;

    public bool LineMiniGameClosed;

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

    private void Start()
    {
    }

    private void Update()
    {
        if (LineMiniGameClosed) {
            MarkDoorAsOpen("Room1Right");
        }
        //switch(currentDay) {
        //case 1:
                
        //    break;
        //case 2:
        //    // code block
        //    break;
        //default:
        //    // code block
        //    break;
        //}
    }

    public bool IsDoorOpen(string doorID) {
        return openedDoors.Contains(doorID);
    }

    public void MarkDoorAsOpen(string doorID)
    {
        if (!IsDoorOpen(doorID))
        {
            openedDoors.Add(doorID);
        }
    }
}
