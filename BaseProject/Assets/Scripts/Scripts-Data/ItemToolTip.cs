using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;

public class ItemToolTip : MonoBehaviour {
    [SerializeField] Text ItemName;
    [SerializeField] Text StatText;

    StringBuilder sb = new StringBuilder();


    public void showTip(EquipableItem item)
    {
        ItemName.text = item.ItemName.ToString();
        sb.Length = 0;
        AddStat(item.DameBonus, "Dame");
        AddStat(item.HealthBonus, "Heal");
        AddStat(item.CritialDameBonus, "CritDame");
        AddStat(item.CritialPercentBonus, "Crit PercenT");
        AddStat(item.DefenseBonus, "Defen");
        StatText.text = sb.ToString();
        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    private void AddStat(float value, string statName)
    {
        if (value != 0)
        {
            if (sb.Length > 0)
            {
                sb.AppendLine();
            }
            if(value > 0)
            {
                sb.Append("+");
            }
            sb.Append(value);
            sb.Append(" ");
            sb.Append(statName);
        }
    }


}
