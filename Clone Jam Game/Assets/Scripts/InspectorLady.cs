using UnityEngine;

public class InspectorLady : MonoBehaviour
{
    public Sprite lookUp;
    public Sprite lookDown;
    public bool isLookingUp;
    public float upSeconds;
    public float downSeconds;
    private float nextEventTime;
    private SpriteRenderer spriteRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = isLookingUp ? lookUp : lookDown;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time < nextEventTime) return;
        isLookingUp ^= true;
        nextEventTime += isLookingUp ? upSeconds : downSeconds;
        spriteRenderer.sprite = isLookingUp ? lookUp : lookDown;
    }
}
