using UnityEngine;

public class FirstHomeSceneManager : MonoBehaviour
{
    public GameObject blackScreen;
    public DialogueBox dialogueBox;
    public Dialogue morningDialogue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(DayCycleManager.Instance.currentDay != 1) return;
        dialogueBox.StartDialogue(morningDialogue);
        blackScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueBox.gameObject.activeInHierarchy) { return; }
        blackScreen.SetActive(false);
    }
}
