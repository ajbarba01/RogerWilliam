using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    Image barImage;

    [SerializeField]
    Health health;

    // Start is called before the first frame update
    void Start()
    {
        // playerHealth = GameObject.FindWithTag("GameController").GetComponent<Health>();
    }

    void Update() {
        SetHealth(health.GetPercentage());
    }

    // Update is called once per frame
    void SetHealth(float percentage)
    {
        barImage.fillAmount = percentage;
    }
}
