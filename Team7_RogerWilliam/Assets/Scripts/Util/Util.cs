using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour
{
    public static Vector3 TowardsMouse(Vector3 origin) {
        Vector3 direction = DistToMouse(origin);
        direction.Normalize();
        
        return direction;
    }

    public static Vector3 DistToMouse(Vector3 origin) {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dist = mouseWorld - origin;
        dist.z = 0;
        return dist;
    }

    public static Quaternion QuaternionOfVector3(Vector3 input, float offset=0f) {
        float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0f, 0f, angle + offset);
        return rot;
    }

    public static Quaternion QuaternionTowardsMouse(Vector3 origin, float offset=0f) {
        Vector3 dir = TowardsMouse(origin);
        return QuaternionOfVector3(dir, offset);
    }

    public static int MouseFacing(Vector3 fromPosition)
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mouseWorld - fromPosition;

        return direction.x >= 0 ? 1 : -1;
    }
}
