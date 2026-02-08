using UnityEngine;
using UnityEngine.Audio;

public class StairsManager : InteractableObject
{
    public DialogueBox dialogueBox;
    public Dialogue dialogue1;
    public Dialogue dialogue2;

    public DayCycleManager DayCycleManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        if (!DayCycleManager.elevatorFalled)
        {
            if (dialogueBox != null) dialogueBox.StartDialogue(dialogue1);
            unityEvent?.Invoke();
        }
        else
        {
            // CutScene
            if (dialogueBox != null) dialogueBox.StartDialogue(dialogue2);
            unityEvent?.Invoke();
        }
    }
}
