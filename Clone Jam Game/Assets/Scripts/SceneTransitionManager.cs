using UnityEngine;

public static class SceneTransitionManager 
{
    public static string TargetSpawnName;

    public static event System.Action OnRoomChanged;

    public static void TriggerRoomChange()
    {
        OnRoomChanged?.Invoke();
    }
}
