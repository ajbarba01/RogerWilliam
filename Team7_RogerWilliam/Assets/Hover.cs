using UnityEngine;

public class Hover : MonoBehaviour
{
    [SerializeField] private Transform art;
    [SerializeField] private Transform shadow;
    [SerializeField] private float hoverDelta = 0.5f;
    [SerializeField] private float hoverCycleTime = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = art.localPosition;
        if (shadow != null) {
            shadow.localPosition = new Vector3(startPos.x, startPos.y - hoverDelta, startPos.z);
        }
    }

    void Update() 
    {
        float cycle = Mathf.Sin((Time.time / hoverCycleTime) * Mathf.PI * 2f);
        art.localPosition = new Vector3(startPos.x, startPos.y + cycle * hoverDelta, startPos.z);
    }
}