using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Inventory : MonoBehaviour {
    [SerializeField] List<Item> startingItems;
    [SerializeField] Transform itemsParents;
    [SerializeField] ItemSlot[] itemSlots;

    public int indexInven;
    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;
    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;
    private void Start()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnRightClickEvent += OnRightClickEvent;
            itemSlots[i].OnBeginDragEvent += OnBeginDragEvent;
            itemSlots[i].OnEndDragEvent += OnEndDragEvent;
            itemSlots[i].OnDragEvent += OnDragEvent;
            itemSlots[i].OnDropEvent += OnDropEvent;
            itemSlots[i].OnPointerEnterEvent += OnPointerEnterEvent;
            itemSlots[i].OnPointerExitEvent += OnPointerExitEvent;
        }
        SetStartingItem();
    }
    private void OnValidate()
    {
        // load ItemSlot for inventory
        if (itemsParents != null)
        {
            itemSlots = itemsParents.GetComponentsInChildren<ItemSlot>();
            for(int i = 0; i < itemSlots.Length; i++)
            {
                itemSlots[i].index = i;
            }
        }
        SetStartingItem();
        //for(int i =0; i < itemSlots.Length; i++)
        //{
        //    Debug.Log(itemSlots[i].Item);
        //}
    }
    //private void RefreshUI()
    //{
    //    int i = 0;
    //    for (; i < items.Count && i < itemSlots.Length; i++)
    //    {
    //        itemSlots[i].Item = items[i];
    //        items[i].indexInven = indexInven;
    //    }

    //    for (; i < itemSlots.Length; i++)
    //    {
    //        itemSlots[i].Item = null;
    //    }
    //}

    private void SetStartingItem()
    {
        int i = 0;
        for (; i < startingItems.Count && i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = startingItems[i];
            startingItems[i].indexInven = indexInven;
        }

        for (; i < itemSlots.Length; i++)
        {
            itemSlots[i].Item = null;
        }
    }
    public bool AddItem(Item item)
    {
        //if (isFull())
        //{
        //    return false;
        //}
        //items.Add(item);
        //RefreshUI();
        //return true;
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if(itemSlots[i].Item == null)
            {
                itemSlots[i].Item = item;
                return true;
            }           
        }
        return false;
    }
    public bool Remove(Item item,int index)
    {
        //if (items.Remove(item))
        //{
        //    RefreshUI();
        //    return true;
        //}
        //return false;
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == item && itemSlots[i].index == index)
            {
                itemSlots[i].Item = null;
                return true;
            }
        }
        return false;
    }
    public bool isFull()
    {
        //return items.Count >= itemSlots.Length;
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].Item == null)
            {
                return false;
            }
        }
        return true;
    }
}
