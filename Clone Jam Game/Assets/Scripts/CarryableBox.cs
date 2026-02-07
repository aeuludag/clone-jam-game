using UnityEngine;

// InteractableObject'ten miras alýyoruz, yani bu AYNI ZAMANDA bir InteractableObject'tir.
public class CarryableBox : InteractableObject
{
    public void PickUp()
    {
        gameObject.SetActive(false); // Gizle
        Debug.Log("Kutu alýndý.");
    }

    public void Drop(Vector3 dropPosition)
    {
        transform.position = dropPosition; // Konumlandýr
        gameObject.SetActive(true);        // Göster
        Debug.Log("Kutu býrakýldý.");
    }
}