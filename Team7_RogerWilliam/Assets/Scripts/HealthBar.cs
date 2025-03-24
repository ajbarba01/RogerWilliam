using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    Image barImage;

    [SerializeField]
    Image tweenImage;

    [SerializeField]
    Health health;

    [SerializeField]
    float lerpSpeed;

    float percentage;
    float animationPercent;

    // Start is called before the first frame update
    void Start()
    {
        percentage = animationPercent = health.GetPercentage();
        // playerHealth = GameObject.FindWithTag("GameController").GetComponent<Health>();
    }

    void Update() {
        float prevPercentage = percentage;
        percentage = health.GetPercentage();
        if (percentage != prevPercentage) {
            if (animationPercent == percentage) {
                animationPercent = prevPercentage;
            }
        }

        if (animationPercent != percentage) {
            animationPercent = Mathf.Lerp(animationPercent, percentage, Time.deltaTime * lerpSpeed);

            if (animationPercent > percentage) {
            barImage.fillAmount = percentage;
            tweenImage.fillAmount = animationPercent;
            }

            if (percentage > animationPercent) {
                barImage.fillAmount = animationPercent;
                tweenImage.fillAmount = percentage;
            }
        }
    }
}
