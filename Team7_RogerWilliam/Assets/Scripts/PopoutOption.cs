using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[RequireComponent(typeof(LoadoutSlot))]
public class PopoutOption : MonoBehaviour
{
    public UnityEvent<LoadoutOption> optionSelected;
    private LoadoutSlot slot;

    private void Awake() {
        slot = GetComponent<LoadoutSlot>();
    }

    public void Clicked() {
        optionSelected.Invoke(GetOption());
    }

    public void SetOption(LoadoutOption option) {
        slot.SetOption(option);
    }

    public LoadoutOption GetOption() {
        return slot.GetOption();
    }
}
