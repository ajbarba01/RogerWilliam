using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float smoothSpeed = 5f;

    void Start()
    {
        player = Player.Instance.transform;
        if (!player) {
            Destroy(gameObject);
        }
        else {
            Vector3 newPos = player.position;
            newPos.z = transform.position.z;
            transform.position = newPos;
        }
    }

    void FixedUpdate()
    {
        if (!player) {
            return;
        }

        Vector3 newPos = Vector3.Lerp(transform.position, player.position, smoothSpeed * Time.deltaTime);
        newPos.z = transform.position.z;
        transform.position = newPos;
    }
}
