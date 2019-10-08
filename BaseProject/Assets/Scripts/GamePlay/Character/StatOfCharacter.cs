using UnityEngine;
using System.Collections;
[System.Serializable]
public class StatOfCharacterTest 
{
    public float value;

    public void ReduceValue(float value)
    {
        if(value > 0)
        {
            this.value -= value;
        }
    }
    public void AddValue(float value)
    {
        if(value > 0)
        {
            this.value += value;
        }
    }
}
