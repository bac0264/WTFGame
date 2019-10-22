using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class DailySlot : MonoBehaviour, IPointerClickHandler
{
    private const int Picked = 0; // Daily picked
    private const int Default = 1; // Daily image default
    private const int Next = 2; // Open next daily
    [SerializeField]
    private int id;
    [SerializeField]
    private bool isRecieve;
    [SerializeField]
    private bool isOpen;

    public DailyPrice price;

    public Transform container;
    public List<GameObject> DailyDisplays;
    public Text day;
    public Text gold;
    // public DailyReward dailyReward;

    public event Action<DailySlot> OnRightClickEvent;
    public int ID
    {
        set
        {
            id = value;
        }
        get
        {
            return id;
        }
    }
    public bool IsRecieve
    {
        set
        {
            isRecieve = value;
            if (isRecieve)
            {
                RecieveReward();
                DailyDisplay(Picked);
            }
        }
        get
        {
            return isRecieve;
        }
    }
    public bool IsOpen
    {
        set
        {
            isOpen = value;
            if (isOpen)
            {
                DailyDisplay(Next);
            }
            else
            {
                DailyDisplay(Default);
            }
        }
        get
        {
            return isOpen;
        }
    }

    public void RecieveReward()
    {
        if(ResourceManager.instance != null)
        {
            ResourceManager.instance.AddResourceNeed(TypeOfResource.Type.Gold, price.Gold);
        }
        if(MenuManager.instance != null)
        {
            MenuManager.instance.RefreshUI();
        }
    }
    public void DailyDisplay(int index)
    {
        for(int i = 0; i < DailyDisplays.Count; i++)
        {
            if (i == index) DailyDisplays[i].SetActive(true);
            else
            {
                DailyDisplays[i].SetActive(false);
            }
        }
    }
    // Onvalidate + Process Event
    #region

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && (eventData.button == PointerEventData.InputButton.Right || eventData.clickCount > 0))
        {
            if (OnRightClickEvent != null)
            {
                OnRightClickEvent(this);
            }
        }
    }
    private void OnValidate()
    {
        if (DailyDisplays.Count == 0)
        {
            for (int i = 0; i < container.childCount; i++)
            {
                GameObject obj = container.transform.GetChild(i).gameObject;
                DailyDisplays.Add(obj);
            }
        }

    }
    #endregion
}
[System.Serializable]
public class DailyPrice
{
    public float Gold;
}

