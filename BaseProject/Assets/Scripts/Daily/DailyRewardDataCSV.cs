using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MENU", menuName = "MENU/dailyRewardDB", order = 1)]
public class DailyRewardDataCSV : ScriptableObject
{
    public List<DailyRewardData> listDataSkill;

    public void LoadDailyReward(List<Dictionary<string, string>> dataCSV)
    {
        listDataSkill = new List<DailyRewardData>();
        for (int i = 0; i < dataCSV.Count; i++)
        {
            DailyRewardData dataEnemy = new DailyRewardData(dataCSV[i]);
            listDataSkill.Add(dataEnemy);
        }
    }
}

[System.Serializable]
public class DailyRewardData
{
    public string DAY;
    public string ID_FREE;
    public string ID_VIP;
    public string REWARD_FREE;
    public string REWARD_VIP_1;
    public string REWARD_VIP_2;
    public DailyRewardData(Dictionary<string, string> data)
    {
        DAY = data["DAY"];
        ID_FREE = data["USERFREE"];
        ID_VIP = data["VIP"];
        REWARD_FREE = data["REWARDFREE"];
        REWARD_VIP_1 = data["REWARDVIP_1"];
        REWARD_VIP_2 = data["REWARDVIP_2"];
    }
}
