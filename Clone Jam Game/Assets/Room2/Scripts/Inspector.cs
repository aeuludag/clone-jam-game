using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inspector : MonoBehaviour
{
    public int harts;
    private bool sceneIsOver;
    private bool hasTakenDamage;

    public GameObject Player;

    public string spawnPointName;
    public string roomToTransform;

    public bool playerIsSafe;

    public DialogueBox dialogueBox;
    public Dialogue dialogue;
    public Dialogue dialogue2;
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
        sceneIsOver = false;
        hasTakenDamage = false;
        if (harts <= 0) {
            harts = 3;
        }
        ToilateDoor.PlayerOnTheToilate += PlayerOnTheToilate;
        ToilateDoor.PlayerExitTheToilate += PlayerExitTheToilate;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = isLookingUp ? lookUp : lookDown;

        playerIsSafe = shadeCount > 0;

        nextEventTime = Time.time + downSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (!sceneIsOver && harts <= 0) {
            sceneIsOver = true;
            StartCoroutine(PunishThePlayer());
            return;
        }

        if (Time.time >= nextEventTime) {
            isLookingUp = !isLookingUp;
            nextEventTime += isLookingUp ? upSeconds : downSeconds;
            spriteRenderer.sprite = isLookingUp ? lookUp : lookDown;

            if (!isLookingUp)
            {
                hasTakenDamage = false;
            }
        }

        playerIsSafe = shadeCount > 0;
         
        if (isLookingUp && !playerIsSafe && !hasTakenDamage)
        {
            hasTakenDamage = true;
            dialogueBox.StartDialogue(dialogue);
            StartCoroutine(CatchPlayerAfterDelay());
            harts--;
        }
    }

    IEnumerator CatchPlayerAfterDelay()
    {
        yield return new WaitForSeconds(waitAfterWarning);

        //if (isLookingUp && !playerIsSafe)
        //{
        //    Debug.Log("GAME OVER: Player was caught!");
        //}
    }

    IEnumerator PunishThePlayer()
    {
        dialogueBox.StartDialogue(dialogue2);
        yield return new WaitForSeconds(waitAfterWarning);

        SceneTransitionManager.TargetSpawnName = spawnPointName;
        SceneManager.LoadScene(roomToTransform);

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
