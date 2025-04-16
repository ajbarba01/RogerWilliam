using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _Instance;

    public static GameAssets Instance {
        get {
            if (_Instance == null) _Instance = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            return _Instance;
        }
    }

    public GameObject damagePopup;
}
