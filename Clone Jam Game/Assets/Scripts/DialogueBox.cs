using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    public Dialogue dialogue;
    public int lineIndex;

    [Header("UI References")]
    public TextMeshProUGUI speakerText;
    public TextMeshProUGUI dialogueText;

    [Header("Settings")]
    public float typeSpeed = 0.05f;
    public float punctuationPause = 0.2f; // Extra wait for . , ! ?

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip typingSound; // Your 8-bit blip sound
    [Range(0, 1)] public float volume = 0.5f;

    private Coroutine typingCoroutine;
    private bool isTyping = false;

    public void NextLine()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.maxVisibleCharacters = dialogue.lines[lineIndex].Length;
            isTyping = false;
            return;
        }

        lineIndex++;
        if (lineIndex > dialogue.lines.Count - 1)
        {
            EndDialogue();
            return;
        }

        DisplayLine(dialogue.lines[lineIndex]);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        gameObject.SetActive(true);
        this.dialogue = dialogue;
        lineIndex = 0;
        speakerText.text = dialogue.speaker;
        DisplayLine(dialogue.lines[0]);
    }

    private void DisplayLine(string line)
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeLine(line));
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        dialogueText.ForceMeshUpdate();

        int totalCharacters = line.Length;

        for (int i = 0; i <= totalCharacters; i++)
        {
            dialogueText.maxVisibleCharacters = i;

            // Handle Audio & Pausing Logic
            if (i < totalCharacters)
            {
                char currentChar = line[i];

                // 1. Play sound (Avoid playing sound for spaces)
                if (currentChar != ' ' && typingSound != null)
                {
                    audioSource.PlayOneShot(typingSound, volume);
                }

                // 2. Wait logic
                if (currentChar != '\'' && char.IsPunctuation(currentChar))
                {
                    yield return new WaitForSeconds(punctuationPause);
                }
                else
                {
                    yield return new WaitForSeconds(typeSpeed);
                }
            }
        }

        isTyping = false;
    }

    public void EndDialogue()
    {
        gameObject.SetActive(false);
    }
}