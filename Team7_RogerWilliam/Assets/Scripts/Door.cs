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

    private void Awake() {
        unlockable.SetActive(false);
    }

    void Start()
    {
        gameHandler = GameObject.FindWithTag("GameController").GetComponent<GameHandler>();
        
        Debug.Log(scene);
        interactable = GetComponentInChildren<Interactable>();
        interactable.onInteract.AddListener(Open);
        // unlockable.SetActive(false);
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

    public void UpdateLock() {
        if (level == LevelManager.currentLevel && NPCGuide.talkedTo) {
            Unlock();
        }
        else if (level < LevelManager.currentLevel) {
            Unlock();
        }
    }
}
