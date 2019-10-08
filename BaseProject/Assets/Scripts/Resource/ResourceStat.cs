using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ResourceStat
{
    public float value;
    public TypeOfResource Type;

    public void AddValue(float value)
    {
        if (value > 0)
            this.value += value;
    }
    public void ReduceValue(float value)
    {
        if (value > 0)
        {
            this.value -= value;
            if (this.value < 0) this.value = 0;
        }
    }
}
