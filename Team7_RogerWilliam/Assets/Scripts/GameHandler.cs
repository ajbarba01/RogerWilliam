using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    public void Controls() {
    }

    public void Play() {
        ChangeScene("LevelOne");
    }

    public void PlayerDeath() {
        Debug.Log("PLAYER DIED");
        ChangeScene("MainMenu");
    }

    public void MainMenu() {
        ChangeScene("MainMenu");
    }

    public void Credits() {
        Debug.Log("ERMMM");
        ChangeScene("CreditsScene");
    }
}
