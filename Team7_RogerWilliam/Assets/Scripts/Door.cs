using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] private string scene;
    private Interactable interactable;
    private GameHandler gameHandler;

    void Start()
    {
        gameHandler = GameObject.FindWithTag("GameController").GetComponent<GameHandler>();
        
        interactable = GetComponent<Interactable>();
        interactable.onInteract.AddListener(Open);
    }
    

    public void Open() {
        gameHandler.ChangeScene(scene);
    }
}
