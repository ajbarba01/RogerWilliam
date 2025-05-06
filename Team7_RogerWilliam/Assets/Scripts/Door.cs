using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class Door : MonoBehaviour
{
    [SerializeField] private Color DEFAULT_LIGHT, NOVEL_LIGHT;

    [SerializeField] private string scene;
    [SerializeField] private GameObject unlockable;
    [SerializeField] private int level;
    [SerializeField] private Light2D light;
    private Interactable interactable;
    private GameHandler gameHandler;

    private bool unlocked = false;

    // private void Awake() {
    //     unlockable.SetActive(false);
    // }

    void Awake()
    {
        gameHandler = GameHandler.Instance;
        
        Debug.Log(scene);
        interactable = GetComponentInChildren<Interactable>();
        interactable.onInteract.AddListener(Open);
        unlockable.SetActive(false);
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
            SetLightColor(NOVEL_LIGHT);
        }
        else if (level < LevelManager.currentLevel) {
            Unlock();
            SetLightColor(DEFAULT_LIGHT);
        }
    }

    private void SetLightColor(Color newColor) {
        light.color = newColor;
    }
}
