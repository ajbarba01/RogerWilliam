using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance;
    public static GameObject gameController;
    public static Health playerHealth;
    public static bool tutorialBossDefeated = false;
    public static bool hotRodDefeated = false;
    public static bool iceCubeDefeated = false;
    public static bool guitarGeorgeDefeated = false;
    public static  bool salamiSamDefeated = false;
    public static bool finalBossDefeated = false;  

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
    public void tutorialBossDeath()
    {
        tutorialBossDefeated = true;
        Debug.Log("BoolCalled!");
    }
    public void hotRodDeath()
    {
        hotRodDefeated = true;
    }
    public void iceCubeDeath()
    {
        iceCubeDefeated = true;
    }
    public void guitarGeorgeDeath()
    {
        guitarGeorgeDefeated = true;
    }
    public void salamiSamDeath()
    {
        salamiSamDefeated = true;
    }
    public void finalBossDeath()
    {
        finalBossDefeated = true;
    }
}
