using UnityEngine;

public class LineZone : MonoBehaviour
{
    public LinePuzzleManager puzzleManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
            
        if (other.CompareTag("CarryableBox"))
        {
            puzzleManager.AddBox();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("CarryableBox"))
        {
            puzzleManager.RemoveBox();
        }
    }
}