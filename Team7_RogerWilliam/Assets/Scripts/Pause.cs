using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : Menu
{
    public static bool isPaused;

    void Start()
    {
        accessKey = KeyCode.Escape;
    }

    public static void Freeze()
    {
        Pause.isPaused = true;
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    public static void Unfreeze()
    {
        Pause.isPaused = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
}
