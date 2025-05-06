using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Interactable))]
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Collider2D dialogueBox;
    [SerializeField] private string[] dialogueOptions;

    public UnityEvent onTalk;
    public UnityEvent onFinishTalk;

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
            Debug.Log("HIDDEN");
            showing = false;
            Dialogue.HideDialogue();
            Dialogue.Instance.dialogueCompleted.RemoveListener(DialogueCompleted);
            Interactions.SetEnabled(true);
        }
    }

    public void DialogueCompleted() {
        onFinishTalk.Invoke();
        Hide();
    }

    private void Show() {
        if (inRange && !showing) {
            onTalk.Invoke();
            showing = true;
            Dialogue.ShowDialogue(dialogueOptions);
            Dialogue.Instance.dialogueCompleted.AddListener(DialogueCompleted);
            Interactions.SetEnabled(false);
        }
    }

    public void SetDialogueOptions(string[] newOptions) {
        dialogueOptions = newOptions;
    }
}
