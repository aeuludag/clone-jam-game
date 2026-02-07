using UnityEngine;

public class Chocolate : MonoBehaviour
{
    public bool isHeld = false;
    public bool isBad = false;
    public float xSpeed = 100f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(isBad)
        {
            GetComponent<SpriteRenderer>().color = Color.darkBlue;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isHeld) return;
        transform.position += new Vector3(xSpeed, 0f, 0f);
    }
}
