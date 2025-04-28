using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject window;
    private string[] dialogueSnippets;
    private int currentdialogue, maxDialogue;

    public UnityEvent dialogueCompleted;

    public static Dialogue Instance; 

    private void Awake() {
        Instance = this;
        HideDialogue();
    }

    private void _ShowDialogue(string[] dialogueEntries) {
        dialogueSnippets = dialogueEntries;
        window.SetActive(true);
        currentdialogue = 0;
        SetText(dialogueSnippets[0]);
        maxDialogue = dialogueEntries.Length;
    }

    private void _HideDialogue() {
        window.SetActive(false);
        currentdialogue = 0;
    }

    public static void ShowDialogue(string[] dialogueEntries) {
        Instance._ShowDialogue(dialogueEntries);
    }

    public static void HideDialogue() {
        Instance._HideDialogue();
    }

    public void NextSnippet() {
        currentdialogue++;
        if (currentdialogue < maxDialogue) {
            SetText(dialogueSnippets[currentdialogue]);
        }

        else {
            HideDialogue();
        }
    }

    public void SetText(string dialogue) {
        dialogueText.text = dialogue;
    }
}
