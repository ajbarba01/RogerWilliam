using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private GameObject window;
    private string[] dialogueSnippets;
    private int currentdialogue, maxDialogue;

    private static Dialogue instance; 

    private void Awake() {
        instance = this;
        HideDialogue();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            string[] entries = {"Hello", "Goodbye"};
            ShowDialogue(entries);
        }
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
        instance._ShowDialogue(dialogueEntries);
    }

    public static void HideDialogue() {
        instance._HideDialogue();
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
