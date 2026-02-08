using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorManager : InteractableObject
{
    public DialogueBox dialogueBox;
    public Dialogue dialogue;

    private AudioSource audioSource;

    public DayCycleManager DayCycleManager;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        if (!DayCycleManager.elevatorFalled)
        {
            audioSource.Play();
            DayCycleManager.elevatorFalled = true;
        }
        else {
            if (dialogueBox != null) dialogueBox.StartDialogue(dialogue);
            unityEvent?.Invoke();
        }
    }
}
