using UnityEngine;
using System.Collections;

public class StatPanel : MonoBehaviour
{
    [SerializeField] StatDisplay[] statDisplays;
    [SerializeField] string[] statNames;
    [SerializeField] private CharacterStat[] stats;


    private void OnValidate()
    {
        statDisplays = GetComponentsInChildren<StatDisplay>();
        // stats = GetComponentsInChildren<CharacterStat>();
        UpdateStatNames();

    }

    public void SetStats(params CharacterStat[] charStats)
    {
        stats = charStats;
        if (stats.Length > statDisplays.Length) return;
        for (int i = 0; i < statDisplays.Length; i++)
        {
            statDisplays[i].gameObject.SetActive(i < stats.Length); 
        }
    }
    public void UpdateStatValues()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            statDisplays[i].ValueText.text = stats[i].Value.ToString();
            Debug.Log(stats[i].Value.ToString());
        }
    }

    public void UpdateStatNames()
    {
        for (int i = 0; i < statNames.Length; i++)
        {
            statDisplays[i].NameText.text = statNames[i];
        }
    }
}
