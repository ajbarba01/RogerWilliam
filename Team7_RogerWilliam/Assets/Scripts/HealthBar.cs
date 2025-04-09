using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private Image tweenImage;
    [SerializeField] private Health health;
    [SerializeField] private float lerpSpeed;

    private float percentage;
    private float animationPercent;

    // Start is called before the first frame update
    void Awake()
    {
        SetHealth(health);
        // playerHealth = GameObject.FindWithTag("GameController").GetComponent<Health>();
    }

    void Update() {
        if (health == null) return;

        percentage = health.GetPercentage();

        if (animationPercent != percentage) {
            animationPercent = Mathf.Lerp(animationPercent, percentage, Time.deltaTime * lerpSpeed);

            if (animationPercent > percentage) {
                barImage.fillAmount = percentage;
                tweenImage.fillAmount = animationPercent;
            }

            else {
                barImage.fillAmount = animationPercent;
                tweenImage.fillAmount = percentage;
            }
        }
    }

    public void SetHealth(Health newHealth) {
        if (newHealth == null) {
            gameObject.SetActive(false);
        }

        else {
            gameObject.SetActive(true);
            health = newHealth;
            percentage = health.GetPercentage();
            animationPercent = percentage;

            barImage.fillAmount = percentage;
            tweenImage.fillAmount = percentage;
        }
    }
}
