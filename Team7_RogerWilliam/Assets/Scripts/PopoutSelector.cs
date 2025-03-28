using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopoutSelector : MonoBehaviour
{

    [SerializeField] private PopoutOption[] options;
    private bool visible = false;
    private int active;

    // Start is called before the first frame update
    void Start()
    {
        foreach (PopoutOption option in options) {
            option.gameObject.SetActive(false);
            option.optionSelected.AddListener(SelectOption);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleVisibility() {
        visible = !visible;

        foreach (PopoutOption option in options) {
            option.gameObject.SetActive(visible);
        }
    }

    public void SelectOption(int option) {
        active = option;
        ToggleVisibility();
    }
}
