using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public static Color CRITICAL = new Color(1, 0, 0);
    public static Color DEFAULT = new Color(1, 1, 1);

    public static DamagePopup Create(Vector3 position, int amount, Color color) {
        GameObject obj = Instantiate(GameAssets.Instance.damagePopup, position, Quaternion.identity);

        DamagePopup dmgPopup = obj.GetComponent<DamagePopup>();
        dmgPopup.Setup(amount, color);

        return dmgPopup;
    }

    [SerializeField] private float dissapearTime, fadeTime, speed;

    private Vector3 direction;

    private TextMeshPro text;

    private float dissapearProgress = 0f;
    private float fadeProgress = 0f;

    private void Awake() {
        text = GetComponent<TextMeshPro>();

        direction = new Vector3(0, 1, 0);
        // Vector2 randomDirection = Random.insideUnitCircle.normalized;
        // direction = new Vector3(randomDirection.x, randomDirection.y, 0f);
    }

    private void Start() {
        StartCoroutine(RunFade());
    }
    
    public void Setup(int amount, Color color) {
        text.text = amount.ToString();
        text.color = color;
    }

    private void Update() {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void ChangeAlpha(float a) {
        Color newColor = text.color;
        newColor.a = a;
        text.color = newColor;
    }

    private IEnumerator RunFade() {
        // while (dissapearProgress < dissapearTime) {
        //     dissapearProgress += Time.deltaTime;
        //     yield return null;
        // }

        yield return new WaitForSeconds(dissapearTime);

        while (fadeProgress < fadeTime) {
            fadeProgress += Time.deltaTime;

            ChangeAlpha(Mathf.Lerp(1f, 0f, fadeProgress / fadeTime));

            yield return null;
        }

        Destroy(gameObject);
    }
}
