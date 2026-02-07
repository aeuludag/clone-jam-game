using UnityEngine;

public class ChocolateSpawner : MonoBehaviour
{
    public GameObject choco;
    public float delaySeconds;
    public float chocoSpeed;
    private float newChocoTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        newChocoTime = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(newChocoTime < Time.time)
        {
            var obj = Instantiate(choco, transform.position + new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0), transform.rotation);
            var theChoco = obj.GetComponent<Chocolate>();
            theChoco.xSpeed = chocoSpeed;
            theChoco.isBad = Random.Range(0f, 1f) >= 0.7f;



            newChocoTime = Time.time + delaySeconds;
        }
    }
}
