using UnityEngine;

public class LinePuzzleManager : MonoBehaviour
{
    [Header("Ayarlar")]
    public int totalBoxesNeeded = 3; 
    private int currentBoxesOnLine = 0;
    public bool done = false;

    public void AddBox()
    {
        currentBoxesOnLine++;
        CheckWin();
    }

    public void RemoveBox()
    {
        currentBoxesOnLine--;
    }

    private void CheckWin()
    {
        Debug.Log("�u an �izgideki kutu: " + currentBoxesOnLine + " / " + totalBoxesNeeded);

        if (currentBoxesOnLine >= totalBoxesNeeded)
        {
            Debug.Log("hepsi �izgide");
            done = true;
        }
    }
}