using System.Collections;
using UnityEngine;

public class Inspector : MonoBehaviour
{
    public GameObject Player;

    public bool playerIsSafe;

    public DialogueBox dialogueBox;
    public Dialogue dialogue;
    public float waitAfterWarning = 1.0f;


    public Sprite lookUp;
    public Sprite lookDown;
    public bool isLookingUp;
    public float upSeconds;
    public float downSeconds;
    private float nextEventTime;
    private SpriteRenderer spriteRenderer;

    private int shadeCount = 0;

    void Start()
    {
        ToilateDoor.PlayerOnTheToilate += PlayerOnTheToilate;
        ToilateDoor.PlayerExitTheToilate += PlayerExitTheToilate;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = isLookingUp ? lookUp : lookDown;

        playerIsSafe = shadeCount > 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < nextEventTime) return;
        isLookingUp ^= true;
        nextEventTime += isLookingUp ? upSeconds : downSeconds;
        spriteRenderer.sprite = isLookingUp ? lookUp : lookDown;

        playerIsSafe = shadeCount > 0;

        if (isLookingUp && !playerIsSafe)
        {
            dialogueBox.StartDialogue(dialogue);
            StartCoroutine(CatchPlayerAfterDelay());
        }
    }

    IEnumerator CatchPlayerAfterDelay()
    {
        yield return new WaitForSeconds(waitAfterWarning);

        if (isLookingUp && !playerIsSafe)
        {
            Debug.Log("GAME OVER: Player was caught!");
        }
    }

    public void EnterShade()
    {
        shadeCount++;
    }

    public void ExitShade()
    {
        shadeCount = Mathf.Max(0, shadeCount - 1);
    }

    public void PlayerOnTheToilate() {
        playerIsSafe = true;
    }

    public void PlayerExitTheToilate() {
        playerIsSafe = false; 
    }

    void OnDestroy()
    {
        ToilateDoor.PlayerOnTheToilate -= PlayerOnTheToilate;
        ToilateDoor.PlayerExitTheToilate -= PlayerExitTheToilate;
    }

}
