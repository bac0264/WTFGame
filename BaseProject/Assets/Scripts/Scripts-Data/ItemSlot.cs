using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler, IBeginDragHandler,IDropHandler
{
    [SerializeField] Image Image;

    public event Action<ItemSlot> OnRightClickEvent;
    public event Action<ItemSlot> OnBeginDragEvent;
    public event Action<ItemSlot> OnEndDragEvent;
    public event Action<ItemSlot> OnDragEvent;
    public event Action<ItemSlot> OnDropEvent;
    public event Action<ItemSlot> OnPointerEnterEvent;
    public event Action<ItemSlot> OnPointerExitEvent;

    private Color normalColor = Color.white;
    private Color disaledColor = new Color(1, 1, 1, 0);

    public int index;
    [SerializeField]  private Item _item;  
    public Item Item
    {
        get { return _item; }
        set {
            _item = value;  
            if(_item == null)
            {
                Image.color = disaledColor;
            }
            else
            { 
                Image.sprite = _item.Icon;
                Image.color = normalColor;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (OnRightClickEvent != null)
            {
                OnRightClickEvent(this); 
            }
        }
    }

    protected virtual void OnValidate()
    {
        // load Image for ItemSlot
        if (Image == null)
        {
            Image = GetComponent<Image>();
        }   

    }
    public virtual bool CanReceiveItem(Item item)
    {
        return true;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (OnPointerEnterEvent != null) OnPointerEnterEvent(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (OnPointerExitEvent != null) OnPointerExitEvent(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragEvent != null) OnDragEvent(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (OnEndDragEvent != null) OnEndDragEvent(this);
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (OnBeginDragEvent != null) OnBeginDragEvent(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (OnDropEvent != null) OnDropEvent(this);
    }
}
