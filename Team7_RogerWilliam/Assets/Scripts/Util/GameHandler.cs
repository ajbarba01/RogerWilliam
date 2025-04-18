using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance;
    public static GameObject gameController;
    public static Health playerHealth;

    void Awake()
    {
        Instance = this;
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
        ChangeScene("Controls");
    }

    public void Play() {
        HomeBase();
    }

    public void PlayerDeath() {
        HomeBase();
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
