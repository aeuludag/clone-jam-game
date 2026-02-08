using Unity.Mathematics;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject box;
    public bool goingRight = true;
    public float xSpeed = 0.01f;
    public float delaySeconds = 1;
    private float newBoxTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        newBoxTime = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(newBoxTime < Time.time)
        {
            var obj = Instantiate(box, transform.position, quaternion.identity);
            var theBox = obj.GetComponent<CarryableBox>();
            theBox.xSpeed = xSpeed;

            newBoxTime = Time.time + delaySeconds;
        }
    }
}
