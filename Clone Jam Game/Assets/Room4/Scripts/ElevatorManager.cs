using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorManager : InteractableObject
{
    public DialogueBox dialogueBox;
    public Dialogue dialogue;

    public AudioSource audioSource;

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

            DayCycleManager.elevatorFalled = true;
        }
        else {
            if (dialogueBox != null) dialogueBox.StartDialogue(dialogue);
            unityEvent?.Invoke();
        }
    }
}
