using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    [SerializeField] private string text = "E to Interact";
    [SerializeField] private KeyCode key = KeyCode.E;
    private bool inRange = false;
    public UnityEvent onInteract;
    private Collider2D interactArea;

    void Update() {
        if (Pause.isPaused) return;
        if (inRange && Input.GetKeyDown(key)) {
            onInteract.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            inRange = true;
            Interactions.Show(text);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            inRange = false;
            Interactions.Hide();
        }
    }
}
