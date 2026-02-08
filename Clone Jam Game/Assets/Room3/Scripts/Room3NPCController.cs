using UnityEngine;
using UnityEngine.UIElements;

public class Room3NPCController : InteractableObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int upper_y;
    public int lower_y;
    public float speed;
    public DialogueBox dialogueBox;
    public Sprite upSprite;
    public Sprite downSprite;
    private SpriteRenderer spriteRenderer;
    private int direction;

    void Start()
    {
        direction = -1;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogueBox.gameObject.activeSelf) {
            transform.position = transform.position + (new Vector3(0, 1, 0)) * direction * speed * Time.deltaTime;
            if (transform.position.y >= upper_y)
            {
                direction = -1;
                spriteRenderer.sprite = downSprite;
            }
            else if (transform.position.y <= lower_y)
            {
                direction = 1;
                spriteRenderer.sprite = upSprite;
            }
        }
    }
}
