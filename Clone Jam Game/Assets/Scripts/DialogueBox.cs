using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    public Dialogue dialogue;
    public int lineIndex;

    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;
    public Button nextButton;

    public void NextLine()
    {
        lineIndex++;
        if(lineIndex > dialogue.lines.Count - 1)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = dialogue.lines[lineIndex];
    }

    public void StartDialogue()
    {
        StartDialogue(dialogue);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        gameObject.SetActive(true);
        this.dialogue = dialogue;
        lineIndex = 0;
        speakerText.text = dialogue.speaker;
        dialogueText.text = dialogue.lines[0];
    }

    public void EndDialogue()
    {
        gameObject.SetActive(false);
    }
}
