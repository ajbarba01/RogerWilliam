using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    public static bool isPaused;
    public static bool locked;

    // Start is called before the first frame update
    void Start()
    {
        ResumeGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (locked) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public static void Freeze()
    {
        Pause.isPaused = true;
        Time.timeScale = 0f;
    }

    public static void Unfreeze()
    {
        Pause.isPaused = false;
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        Freeze();
        pauseMenu.SetActive(true);
        // AudioListener.pause = true;
    }

    public void ResumeGame()
    {
        Unfreeze();
        pauseMenu.SetActive(false);
        AudioListener.pause = false;
    }

    public static void Lock() {
        locked = true;
    }

    public static void Unlock() {
        locked = false;
    }
}
