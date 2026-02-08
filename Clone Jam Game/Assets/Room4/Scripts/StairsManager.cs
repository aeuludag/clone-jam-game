using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class StairsManager : InteractableObject
{
    public DialogueBox dialogueBox;
    public Dialogue dialogue1;
    public Dialogue dialogue2;

    public VideoClip cutsceneVideo;
    public string videoName;

    public DayCycleManager DayCycleManager;

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
            if (!string.IsNullOrEmpty(videoName))
            {
                SceneManager.LoadScene(videoName);
            }

            if (dialogueBox != null) dialogueBox.StartDialogue(dialogue2);
            unityEvent?.Invoke();
        }
    }
}
