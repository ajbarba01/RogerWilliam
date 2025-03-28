using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private Collider2D interactArea;

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) {
        Interactions.Show();
    }

    private void OnTriggerExit2D(Collider2D other) {
        Interactions.Hide();
    }
}
