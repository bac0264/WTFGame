using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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
}
public class ShowResource
{
    public static void Show(Text ResourceText, ResourceStat resource)
    {
        if (resource != null)
            ResourceText.text = resource.value.ToString();
    }
}
