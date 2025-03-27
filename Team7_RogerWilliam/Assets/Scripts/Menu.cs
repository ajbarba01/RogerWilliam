using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Menu : MonoBehaviour
{
    public KeyCode accessKey;
    public bool isOpen;
    public GameObject menu;

    public void Open() {
        isOpen = true;
        OnOpen();
    }
    public void Close() {
        isOpen = false;
        OnClose();
    }

    protected virtual void OnOpen() {}
    protected virtual void OnClose() {}
}
