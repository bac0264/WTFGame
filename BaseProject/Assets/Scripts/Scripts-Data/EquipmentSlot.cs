using UnityEngine;
using System.Collections;

public class EquipmentSlot : ItemSlot
{
    public EquipmentType equipmentType;
    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = equipmentType.ToString() + " Slot";
    }
    public override bool CanReceiveItem(Item item)
    {
        if(item == null)
        {
            return true;
        }
        EquipableItem equipable = item as EquipableItem;
        return equipable != null && equipable.equipmentType == equipmentType;
    }
}
