using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 25f;
    private GameHandler gameHandler;

    // Start is called before the first frame update
    void Start()
    {
        gameHandler = GameObject.FindWithTag("GameController").GetComponent<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            gameHandler.MainMenu();
        }

        //moving credits upwards so it looks cute
        transform.position += new Vector3(0, scrollSpeed * Time.deltaTime, 0);
    }
}
