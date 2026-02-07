using UnityEngine;

public class ChocolateEater : MonoBehaviour
{
    public HandController hand;
    public bool acceptsBad;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("COLLLISIONN");
        var choco = collision.gameObject.GetComponent<Chocolate>();
        if(choco == null) Destroy(collision.gameObject);

        if(choco.isBad == acceptsBad) { hand.IncreasePoint(); } else {hand.DecreasePoint();}
        Destroy(collision.gameObject);
    }
}
