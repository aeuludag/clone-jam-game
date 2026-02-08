using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject box;
    public bool goingRight = true;
    public float xSpeed = 0.1f;
    
    void Start()
    {
        //TODO BOX OBJECT!!
    }


    void Update()
    {
        transform.position += new Vector3(xSpeed, 0f, 0f);
    }
}
