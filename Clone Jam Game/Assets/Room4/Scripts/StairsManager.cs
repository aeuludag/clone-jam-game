using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class StairsManager : InteractableObject
{
    public Player player;

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
            //if (dialogueBox != null) dialogueBox.StartDialogue(dialogue2);
            //unityEvent?.Invoke();

            // CutScene
            if (!string.IsNullOrEmpty(videoName))
            {
                Destroy(player);
                SceneManager.LoadScene(videoName);
            }
        }
    }
}
