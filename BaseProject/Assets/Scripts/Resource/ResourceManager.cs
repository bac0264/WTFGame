using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;

    public ResourceStat[] Resources;

    private void Awake()
    {
        LoadResource();
    }
    public void LoadResource()
    {
        // TO DO somethin....
        if (Resources.Length > 0)
        {
            foreach (ResourceStat resource in Resources)
            {
                Debug.Log((int)resource.Type.type);
            }
        }
    }
    public void SaveResouce()
    {
        // TO DO somethin....
        foreach (ResourceStat resource in Resources)
        {
            Debug.Log(resource.Type.type + ", " + resource.value);
        }
    }
    public bool ReduceResourceNeed(TypeOfResource.Type type, float Value)
    {
        ResourceStat resourceNeed = getResourceNeed(type);
        if (resourceNeed != null && Value > 0)
        {
            resourceNeed.ReduceValue(Value);
            return true;
        }
        return false;
    }
    public bool AddResourceNeed(TypeOfResource.Type type, float Value)
    {
        ResourceStat resourceNeed = getResourceNeed(type);
        if (resourceNeed != null && Value > 0)
        {
            resourceNeed.AddValue(Value);
            return true;
        }
        return false;
    }
    public ResourceStat getResourceNeed(TypeOfResource.Type type)
    {
        if ((int)type < Resources.Length)
            return Resources[(int)type];
        return null;
    }

    public void ReduceGold()
    {
        ResourceStat Gold = getResourceNeed(TypeOfResource.Type.Gold);
        Debug.Log("Gold: " + Gold.value + ", " + Gold.Type.type);
        Gold.ReduceValue(1);
        Debug.Log("ReduceGold: " + Resources[(int)TypeOfResource.Type.Gold].value + ", "
            + Resources[(int)TypeOfResource.Type.Gold].Type.type);
        SaveResouce();
    }
    public void ReduceGem()
    {
        ResourceStat Gem = getResourceNeed(TypeOfResource.Type.Gem);
        Debug.Log("Gold: " + Gem.value + ", " + Gem.Type.type);
        Gem.ReduceValue(1);
        Debug.Log("ReduceGold: " + Resources[(int)TypeOfResource.Type.Gem].value + ", "
            + Resources[(int)TypeOfResource.Type.Gem].Type.type);
        SaveResouce();
    }
    public void ReduceExp()
    {
        ResourceStat Exp = getResourceNeed(TypeOfResource.Type.Exp);
        Debug.Log("Gold: " + Exp.value + ", " + Exp.Type.type);
        Exp.ReduceValue(1);
        Debug.Log("ReduceGold: " + Resources[(int)TypeOfResource.Type.Exp].value + ", "
            + Resources[(int)TypeOfResource.Type.Exp].Type.type);
        SaveResouce();
    }
}
