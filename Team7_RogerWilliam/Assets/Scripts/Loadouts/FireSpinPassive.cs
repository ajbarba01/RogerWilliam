using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpinPassive : Passive
{
    [SerializeField] private GameObject fireSpinner;
    [SerializeField] private int numSpinners;
    [SerializeField] private float radius = 1.5f;
    [SerializeField] private float spinSpeed = 90f; // degrees per second

    private GameObject[] spinners;
    private float currentAngle;

    private void Awake() {
        spinners = new GameObject[numSpinners];
        for (int i = 0; i < numSpinners; i++) {
            spinners[i] = Instantiate(fireSpinner, transform.position, Quaternion.identity, transform);
        }
    }

    private void Update()
    {
        float angleStep = 360f / numSpinners;
        currentAngle += spinSpeed * Time.deltaTime;

        for (int i = 0; i < numSpinners; i++)
        {
            float angle = currentAngle + i * angleStep;
            float rad = angle * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius;
            spinners[i].transform.position = transform.position + offset;
        }
    }
}