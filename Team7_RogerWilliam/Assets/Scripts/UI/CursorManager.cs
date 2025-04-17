using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private static CursorManager instance;

    [SerializeField] private Texture2D cursorHover;
    [SerializeField] private Texture2D cursorClick;

    private bool clicked = false;

    private Vector2 cursorHotspot;

    private void Awake() {
    }

    void Start()
    {
        cursorHotspot = new Vector2(cursorHover.width / 2, cursorHover.height / 2);
        Cursor.SetCursor(cursorHover, cursorHotspot, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if (!clicked) {
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
            {
                Cursor.SetCursor(cursorClick, cursorHotspot, CursorMode.Auto);
                clicked = true;
            }
        }
        else if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
        {
            Cursor.SetCursor(cursorHover, cursorHotspot, CursorMode.Auto);
            clicked = false;
        }
    }
}
