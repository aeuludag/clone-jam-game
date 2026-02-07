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

    void Start()
    {
        Shade.PlayerEnterShade += PlayerUnderShade;
        Shade.PlayerExitShade += PlayerExitShade;
        ToilateDoor.PlayerOnTheToilate += PlayerOnTheToilate;
        ToilateDoor.PlayerExitTheToilate += PlayerExitTheToilate;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = isLookingUp ? lookUp : lookDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < nextEventTime) return;
        isLookingUp ^= true;
        nextEventTime += isLookingUp ? upSeconds : downSeconds;
        spriteRenderer.sprite = isLookingUp ? lookUp : lookDown;

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

    public void PlayerExitShade() {
        if (playerIsSafe) {
            playerIsSafe = false;
        }
    }

    public void PlayerUnderShade() {
        if (!playerIsSafe){
            playerIsSafe = true;
        }
    }

    public void PlayerOnTheToilate() {
        playerIsSafe = true;
    }

    public void PlayerExitTheToilate() {
        playerIsSafe = false; 
    }

    void OnDestroy()
    {
        Shade.PlayerEnterShade -= PlayerUnderShade;
        Shade.PlayerExitShade -= PlayerExitShade;
        ToilateDoor.PlayerOnTheToilate -= PlayerOnTheToilate;
        ToilateDoor.PlayerExitTheToilate -= PlayerExitTheToilate;
    }

}
