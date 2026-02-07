using UnityEngine;

public class LinePuzzleManager : MonoBehaviour
{
    [Header("Ayarlar")]
    public int totalBoxesNeeded = 3; 
    private int currentBoxesOnLine = 0;

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
        Debug.Log("Þu an çizgideki kutu: " + currentBoxesOnLine + " / " + totalBoxesNeeded);

        if (currentBoxesOnLine >= totalBoxesNeeded)
        {
            Debug.Log("hepsi çizgide");
        }
    }
}