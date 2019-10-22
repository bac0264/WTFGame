using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    public Text Menu_Gold_Text;
    public Text Daily_Gold_Text;
    private void Awake()
    {
        if (instance == null) instance = this;
        if (!PlayerPrefs.HasKey(KeySave.IS_THE_FIRST_TIME))
        {
            PlayerPrefs.SetInt(KeySave.IS_THE_FIRST_TIME, 0);
        }
        else
        {
            if (DailyManager.instance != null)
                DailyManager.instance.dailyPanel.LoadData();
        }
    }
    private void Start()
    {
        RefreshUI();
    }
    private void OnValidate()
    {
        if (Menu_Gold_Text == null) Menu_Gold_Text = GameObject.Find("Menu_Gold_Text").GetComponent<Text>();
        if (Daily_Gold_Text == null) Daily_Gold_Text = GameObject.Find("Daily_Gold_Text").GetComponent<Text>();
    }
    public void RefreshUI()
    {
        if (ResourceManager.instance != null)
        {
            ShowResource.Show(Menu_Gold_Text, ResourceManager.instance.getResourceNeed(TypeOfResource.Type.Gold));
            ShowResource.Show(Daily_Gold_Text, ResourceManager.instance.getResourceNeed(TypeOfResource.Type.Gold));
        }
    }
}
