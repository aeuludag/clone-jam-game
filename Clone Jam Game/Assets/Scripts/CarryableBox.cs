using UnityEngine;

// InteractableObject'ten miras al�yoruz, yani bu AYNI ZAMANDA bir InteractableObject'tir.
public class CarryableBox : InteractableObject
{
    public float xSpeed;
    public void FixedUpdate()
    {
        transform.position += new Vector3(xSpeed, 0f, 0f);
        if(transform.position.x > 15) Destroy(this.gameObject);
    }
    public void PickUp()
    {
        xSpeed = 0;
        gameObject.SetActive(false); // Gizle
        Debug.Log("Kutu al�nd�.");
    }

    public void Drop(Vector3 dropPosition)
    {
        transform.position = dropPosition; // Konumland�r
        gameObject.SetActive(true);        // G�ster
        Debug.Log("Kutu b�rak�ld�.");
    }
}