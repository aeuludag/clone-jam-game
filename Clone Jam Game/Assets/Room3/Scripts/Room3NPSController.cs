using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Rendering.MaterialUpgrader;

public class Room3NPSController : InteractableObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int upper_y;
    public int lower_y;
    public float speed;
    public string NpcName;
    public string dialogueText;

    public bool speaking;


    private int direction;

    void Start()
    {
        direction = -1;
        speaking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!speaking) {
            transform.position = transform.position + (new Vector3(0, 1, 0)) * direction * speed * Time.deltaTime;
            if (transform.position.y >= upper_y)
            {
                direction = -1;

            }
            else if (transform.position.y <= lower_y)
            {
                direction = 1;
            }
        }
    }

    public override void Interact()
    {
        Debug.Log($"Talking to {NpcName}: {dialogueText}");
        //TODO
        StartSpeak();
    }

    // CALL THESE FUNCTIONS FROM PLAYER
    public void StartSpeak() {
        speaking = true;
        //TODO
    }

    public void StopSpeaking() {
        speaking = false;
        //TODO
    }
}
