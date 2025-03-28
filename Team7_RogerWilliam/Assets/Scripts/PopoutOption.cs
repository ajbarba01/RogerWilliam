using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PopoutOption : MonoBehaviour
{
    public UnityEvent<int> optionSelected;
    public int identity;

    public void Clicked() {
        optionSelected.Invoke(identity);
    }
}
