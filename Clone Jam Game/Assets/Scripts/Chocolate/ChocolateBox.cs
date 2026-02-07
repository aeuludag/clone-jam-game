using UnityEngine;

public class ChocolateBox : MonoBehaviour
{
    public int chocolateCount = 0;
    public bool isTypeBad = false;
    public void AddChocolate(Chocolate chocolate)
    {
        if(isTypeBad == chocolate.isBad) chocolateCount++;
        Destroy(chocolate.gameObject);
    }
}
