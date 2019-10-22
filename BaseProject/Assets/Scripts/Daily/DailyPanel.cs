
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyPanel : MonoBehaviour
{
    public DailySlot[] slots;
    public event Action<DailySlot> OnRightClickEvent;
    private void OnValidate()
    {
        if (slots.Length == 0)
        {
            slots = GetComponentsInChildren<DailySlot>();
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].name = "Slot " + (i + 1);
                slots[i].day.name = "Day ";
                slots[i].day.text = "Day " + (i + 1);
                slots[i].gold.text = slots[i].price.Gold.ToString();
                slots[i].ID = i;
            }
        }
    }
    private void OnDisable()
    {
        SaveData();
    }
    private void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].OnRightClickEvent += OnRightClickEvent;
        }
    }

    public void LoadData()
    {
        DailyPanelSaveLoadData.LoadResource(this);
    }
    public void SaveData()
    {
        DailyPanelSaveLoadData.SaveResouce(this);
    }

    public void Clear()
    {
        foreach(DailySlot daily in slots)
        {
            daily.IsOpen = false;
            daily.IsRecieve = false;
        }
    }
    public bool IsRecieveAll()
    {
        foreach (DailySlot daily in slots)
        {
            if (!daily.IsRecieve) return false;
        }
        return true;
    }
    public DailySlot getSlot(int index)
    {
        if (index >= slots.Length || index < 0) return null;
        return slots[index];
    }
}

public class DailyPanelSaveLoadData
{
    public static void LoadResource(DailyPanel dailyPanel)
    {
        // TO DO somethin....
        dailyPanel.Clear();
        string[] s = PlayerPrefsX.GetStringArray(KeySave.DAILY_REWARD);
        for (int i = 0; i < s.Length && i < dailyPanel.slots.Length ; i++)
        {
            DailySlot slot = dailyPanel.slots[i];
            string[] temp = s[i].Split(',');
            for(int j = 0; j < temp.Length; j++)
            {
                slot.ID = int.Parse(temp[j]);
                slot.IsOpen = Boolean.Parse(temp[++j]);
                slot.IsRecieve = Boolean.Parse(temp[++j]);
            }
        }
    }
    public static void SaveResouce(DailyPanel dailyPanel)
    {
        // TO DO somethin....
        List<string> s = new List<string>();
        int i = 0;
        foreach (DailySlot dailySlot in dailyPanel.slots)
        {
            s.Add(dailySlot.ID.ToString()+","+
                     dailySlot.IsOpen.ToString()+","
                    + dailySlot.IsRecieve.ToString());
            i++;
        }
        PlayerPrefsX.SetStringArray(KeySave.DAILY_REWARD, s.ToArray());
    }
}
