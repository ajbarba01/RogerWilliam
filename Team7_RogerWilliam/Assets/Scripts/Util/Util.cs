using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour
{
    public static Vector3 TowardsMouse(Vector3 origin) {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mouseWorld - origin;
        direction.z = 0;
        direction.Normalize();
        
        return direction;
    }

    public static Quaternion QuaternionOfVector3(Vector3 input, float offset=0f) {
        float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0f, 0f, angle + offset);
        return rot;
    }
}
