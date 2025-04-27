using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Collider2D dialogueBox;
    [SerializeField] private string[] dialogueOptions;

    private Interactable interactable;
    private bool showing;
    private bool inRange;

    private void Awake() {
        inRange = false;
        showing = false;
        interactable = GetComponent<Interactable>();
        interactable.onInteract.AddListener(Show);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            inRange = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if (showing) {
            inRange = false;
            Hide();
        }
    }

    private void Hide() {
        if (showing) {
            showing = false;
            Dialogue.HideDialogue();
            Dialogue.Instance.dialogueCompleted.RemoveListener(Hide);
            Interactions.SetEnabled(true);
        }
    }

    private void Show() {
        if (inRange && !showing) {
            showing = true;
            Dialogue.ShowDialogue(dialogueOptions);
            Dialogue.Instance.dialogueCompleted.AddListener(Hide);
            Interactions.SetEnabled(false);
        }
    }
}
