using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;
    public static GameObject gameController;
    public static Health playerHealth;

    void Awake()
    {
        instance = this;
        gameController = gameObject;
        playerHealth = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit() {
        Application.Quit();
    }

    public void ChangeScene(string newScence) {
        SceneManager.LoadScene(newScence);
        Pause.Unfreeze();
    }

    public void Controls() {
    }

    public void Play() {
        HomeBase();
    }

    public void PlayerDeath() {
        Debug.Log("PLAYER DIED");
        ChangeScene("MainMenu");
    }

    public void MainMenu() {
        ChangeScene("MainMenu");
    }

    public void Credits() {
        ChangeScene("CreditsScene");
    }

    public void HomeBase() {
        ChangeScene("HomeBase");
    }
}
