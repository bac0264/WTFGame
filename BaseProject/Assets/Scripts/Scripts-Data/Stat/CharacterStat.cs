
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

/// <summary>
/// Represent Stat Modifiers
/// </summary>
/// 
[Serializable]
public class CharacterStat
{
    public float BaseValue;
    public virtual float Value
    {
        get
        {
            if (isDirty || BaseValue != lastBaseValue)
            {
                lastBaseValue = BaseValue;
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }

    private bool isDirty = true; // indicate if needing to recalculate the value or not
    private float _value;
    private float lastBaseValue = float.MinValue;

    private readonly List<StatModifier> statModifiers;
    public readonly ReadOnlyCollection<StatModifier> StatModifiers;
    public CharacterStat()
    {
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();
    }
    public CharacterStat(float BaseValue) : this()
    {
        this.BaseValue = BaseValue;
    }

    /// <summary>
    /// Receive as an input parameter the modifier
    /// Change the AddModifier method
    /// </summary>
    public virtual void AddModifier(StatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
    }
    // And change the RemoveModifier method
    public virtual bool RemoveModifier(StatModifier mod)
    {
        if (statModifiers.Remove(mod)) {
            isDirty = true;
            return true;
        }
        return false;

    }
    public virtual bool RemoveAllModifierFromSource(object source)
    {
        bool didRemove = false;
        for (int i = statModifiers.Count - 1; i >= 0; i--) {
            if(statModifiers[i].Source == source)
            {
                isDirty = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }
    public virtual float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float sumPercenAdd = 0;
        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];
            if (mod.Type == StatModType.Flat)
            {
                finalValue += statModifiers[i].Value;
            }
            else if(mod.Type == StatModType.PercentAdd)
            {
                sumPercenAdd *= 1 + mod.Value;
                if( (i+1) >= statModifiers.Count || statModifiers[i+1].Type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumPercenAdd;
                    sumPercenAdd = 0;
                }
            }
            else if(mod.Type == StatModType.PercentMult)
            {
                finalValue *= 1 + mod.Value;
            }
        }

        return (float)Math.Round(finalValue, 4);
    }

    public virtual int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order) return -1;
        else if (a.Order > b.Order) return 1;
        return 0; // ( a.Order == b.Order)
    }
}
