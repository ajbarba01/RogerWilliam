using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passive : Loadout
{
    protected string targetTag = "Enemy";

    protected Transform target;
    public void setTargetTag(string tag)
    {
        targetTag = tag;
    }
    public string getTargetTag()
    {
        return targetTag;
    }

    public void SetTarget(Transform newTarget) 
    {
        target = newTarget;
    }
}
