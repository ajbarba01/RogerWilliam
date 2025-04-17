using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PopoutSelector : MonoBehaviour
{

    [SerializeField] private PopoutOption[] options;
    [SerializeField] private TextMeshProUGUI title;
    private HashSet<LoadoutOption> data;
    private bool visible = false;
    private LoadoutOption active;

    public UnityEvent<LoadoutOption> newOption;

    void Start()
    {
        foreach (PopoutOption option in options) {
            option.gameObject.SetActive(false);
            option.optionSelected.AddListener(SelectOption);
        }

        title.gameObject.SetActive(true);
    }

    public void ToggleVisibility() {
        visible = !visible;

        if (visible) {
            int i = 0;
            foreach (LoadoutOption option in data) {
                if (active.GetName() == option.GetName()) {
                    continue;
                }

                options[i].gameObject.SetActive(true);
                options[i].SetOption(option);
                i++;
            }

            if (i != 0) {
                title.gameObject.SetActive(false);
            }
        }

        else {
            foreach (PopoutOption option in options) {
                option.gameObject.SetActive(false);
            }

            title.gameObject.SetActive(true);
        }
    }

    public void SelectOption(LoadoutOption option) {
        SetActive(option);
        newOption.Invoke(active);
        ToggleVisibility();
    }

    public void SetActive(LoadoutOption newActive) {
        active = newActive;
    }

    public void SetOptions(HashSet<LoadoutOption> newOptions) {

        data = newOptions;
        return;
    }
}
