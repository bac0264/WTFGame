using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Character : MonoBehaviour
{

    public CharacterStat Damage;
    public CharacterStat Heal;
    public CharacterStat CritDame;
    public CharacterStat CritPercent;
    public CharacterStat Defense;


    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] StatPanel statPanel;
    [SerializeField] ItemToolTip itemTooltip;
    [SerializeField] Image draggableItem;

    private ItemSlot draggedSlot;
    private void OnValidate()
    {
        if (itemTooltip == null)
            itemTooltip = FindObjectOfType<ItemToolTip>();
    }
    private void Awake()
    {
        statPanel.SetStats(Damage, Heal, CritDame, CritPercent, Defense);
        statPanel.UpdateStatValues();

        // Set up event
        // Right click
        inventory.OnRightClickEvent += Equip;
        equipmentPanel.OnRightClickEvent += Unequip;

        // Pointer enter
        inventory.OnPointerEnterEvent += ShowToolTip;
        equipmentPanel.OnPointerEnterEvent += ShowToolTip;

        // Pointer Exit
        inventory.OnPointerExitEvent += HideToolTip;
        equipmentPanel.OnPointerExitEvent += HideToolTip;

        //// BeginDrag
        //inventory.OnBeginDragEvent += BeginDrag;
        //equipmentPanel.OnBeginDragEvent += BeginDrag;

        //// EndDrag
        //inventory.OnEndDragEvent += EndDrag;
        //equipmentPanel.OnEndDragEvent += EndDrag;

        //// Drag
        //inventory.OnDragEvent += Drag;
        //equipmentPanel.OnDragEvent += Drag;

        //// Drop
        //inventory.OnDropEvent += Drop;
        //equipmentPanel.OnDropEvent += Drop;
    }

    //private void EquipFromInventory(Item item)
    //{
    //    if (item is EquipableItem)
    //    {
    //        Equip((EquipableItem)item);
    //    }
    //}
    //private void UnequipFromEquipPanel(Item item)
    //{
    //    if (item is EquipableItem)
    //    {
    //        Unequip((EquipableItem)item);
    //    }
    //}
    private void Equip(ItemSlot itemslot)
    {
        EquipableItem equipItem = itemslot.Item as EquipableItem;
        if (equipItem != null)
        {
            Equip(equipItem, itemslot.index);
        }
    }
    private void Unequip(ItemSlot itemslot)
    {
        EquipableItem equipItem = itemslot.Item as EquipableItem;
        if (equipItem != null)
        {
            Unequip(equipItem);
        }
    }
    public void Equip(EquipableItem item,int index)
    {
        if (inventory.Remove(item, index))
        {
            EquipableItem preItem;
            if (equipmentPanel.AddItem(item, out preItem))
            {
                if (preItem != null)
                {
                    inventory.AddItem(preItem);
                    preItem.Unequip(this);
                    statPanel.UpdateStatValues();
                }
                item.Equip(this);
                statPanel.UpdateStatValues();
                itemTooltip.HideTooltip();
            }
            else inventory.AddItem(item);
        }
    }
    public void Unequip(EquipableItem item)
    {
        if (!inventory.isFull() && equipmentPanel.RemoveItem(item))
        {
            item.Unequip(this);
            statPanel.UpdateStatValues();
            inventory.AddItem(item);
        }
    }
    public void ShowToolTip(ItemSlot itemslot)
    {
        EquipableItem equipItem = itemslot.Item as EquipableItem;
        if (equipItem != null)
        {
            itemTooltip.showTip(equipItem);
        }
    }
    public void HideToolTip(ItemSlot itemslot)
    {
        EquipableItem equipItem = itemslot.Item as EquipableItem;
        if (equipItem != null)
        {
            itemTooltip.HideTooltip();
        }
    }
    public void BeginDrag(ItemSlot itemslot)
    {
        if (itemslot.Item != null)
        {
            draggedSlot = itemslot;
            draggableItem.sprite = itemslot.Item.Icon;
            draggableItem.transform.position = Input.mousePosition;
            draggableItem.enabled = true;
        }
    }
    public void EndDrag(ItemSlot itemslot)
    {
        draggedSlot = null;
        draggableItem.enabled = false;
    }
    public void Drag(ItemSlot itemslot)
    {
        if (draggableItem.enabled)
        {
            draggableItem.transform.position = Input.mousePosition;
        }
    }
    public void Drop(ItemSlot dropSlot)
    {
        if (dropSlot.CanReceiveItem(draggedSlot.Item) && draggedSlot.CanReceiveItem(dropSlot.Item))
        {
            EquipableItem dragItem = draggedSlot.Item as EquipableItem;
            EquipableItem dropItem = dropSlot.Item as EquipableItem;
            if (draggedSlot is EquipmentSlot)
            {
                if (dragItem != null)
                {
                    dragItem.Unequip(this);
                }
                if (dropItem != null)
                {
                    dropItem.Equip(this);
                }
            }
            Debug.Log("Dropitem: " + dropItem);
            if (dropSlot is EquipmentSlot)
            {
                if (dragItem != null)
                {
                    dragItem.Equip(this);
                }
                if (dropItem != null)
                {
                    dropItem.Unequip(this);
                }
            }
            Debug.Log("drag: " + dragItem.DameBonus);
            Debug.Log("drag: " + dragItem.CritialDameBonus);
            Debug.Log("drag: " + dragItem.CritialPercentBonus);
            Debug.Log("drag: " + dragItem.HealthBonus);

            //Debug.Log("drop: " + dropItem.DameBonus);
            //Debug.Log("drop: " + dropItem.CritialDameBonus);
            //Debug.Log("drop: " + dropItem.CritialPercentBonus);
            //Debug.Log("drop: " + dropItem.HealthBonus);

            statPanel.UpdateStatValues();
            Item draggedItem = draggedSlot.Item;
            draggedSlot.Item = dropSlot.Item;
            dropSlot.Item = draggedItem;
        }
    }
}

