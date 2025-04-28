using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    [SerializeField] private string scene;
    [SerializeField] private GameObject unlockable;
    [SerializeField] private int level;
    private Interactable interactable;
    private GameHandler gameHandler;

    private bool unlocked = false;

    void Start()
    {
        gameHandler = GameObject.FindWithTag("GameController").GetComponent<GameHandler>();
        
        interactable = GetComponentInChildren<Interactable>();
        interactable.onInteract.AddListener(Open);
        unlockable.SetActive(false);
        
        if (level <= LevelManager.Instance.currentLevel) {
            Unlock();
        }
    }
    

    public void Open() {
        if (unlocked) {
            gameHandler.ChangeScene(scene);
        }
    }

    public void Unlock() {
        unlocked = true;
        unlockable.SetActive(true);
    }
}
