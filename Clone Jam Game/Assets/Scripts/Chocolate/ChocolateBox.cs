using UnityEngine;

public class ChocolateBox : MonoBehaviour
{
    public HandController hand;
    public bool isTypeBad = false;
    public void AddChocolate(Chocolate chocolate)
    {
        if (isTypeBad == chocolate.isBad) { hand.IncreasePoint(); } else { hand.DecreasePoint(); }
        Destroy(chocolate.gameObject);
    }
}
