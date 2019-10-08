using UnityEngine;
using System.Collections;

public enum EquipmentType
{
    Armor,
    Weapon,
    Necklace,
    Ring
}
[CreateAssetMenu]
public class EquipableItem : Item
{
    public int DameBonus;
    public int CritialDameBonus;
    public int CritialPercentBonus;
    public int HealthBonus;
    public int DefenseBonus;

    [Space]
    public float DamePercentBonus;
    public float CritialDamePercentBonus;
    public float CritialPercentDoubleBonus;
    public float HealthBonusPercentBonus;
    public float DefensePercentBonus;
    [Space]

    public EquipmentType equipmentType;

    public void Equip(Character c)
    {
        if(DameBonus != 0)
        {
            c.Damage.AddModifier(new StatModifier(DameBonus, StatModType.Flat, this));
        }
        if (CritialDameBonus != 0)
        {
            c.CritDame.AddModifier(new StatModifier(CritialDameBonus, StatModType.Flat, this));
        }
        if (CritialPercentBonus != 0)
        {
            c.CritPercent.AddModifier(new StatModifier(CritialPercentBonus, StatModType.PercentMult, this));
        }
        if (HealthBonus != 0)
        {
            c.Heal.AddModifier(new StatModifier(HealthBonus, StatModType.Flat, this));
        }
        if (DefenseBonus != 0)
        {
            c.Defense.AddModifier(new StatModifier(DefenseBonus, StatModType.Flat, this));
        }
    }
    public void Unequip(Character c)
    {
        c.CritDame.RemoveAllModifierFromSource(this);
        c.Damage.RemoveAllModifierFromSource(this);
        c.Defense.RemoveAllModifierFromSource(this);
        c.CritPercent.RemoveAllModifierFromSource(this);
        c.Heal.RemoveAllModifierFromSource(this);
    }
}
