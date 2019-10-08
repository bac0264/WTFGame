using UnityEngine;
using System.Collections;

[System.Serializable]
public class TypeOfResource
{
    public enum Type
    {
        Gold,
        Gem,
        Exp,
    }
    public Type type;
}
