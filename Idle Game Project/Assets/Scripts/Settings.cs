using UnityEngine;
using TMPro;
using static Methods;

public class Settings : MonoBehaviour
{

    public static Settings instance;
    private void Awake() => instance = this;

    public string[] NotationNames;

    public TMP_Text[] SettingText;

    public GameObject[] SettingPanels;
    public void StartSettings()
    {
        NotationNames = new[] { "Standard", "Scientific", "Engineering","Log"};
        Notation = Controller.instance.Data.notation;
        SyncSetting();
    }

    public void ChangeSetting(string settingName)
    {
        var Data = Controller.instance.Data;
        switch (settingName)
        {
            case "Notation":
                Data.notation++;
                if (Data.notation > NotationNames.Length - 1) Data.notation = 0;
                Notation = Data.notation;
                break;
        }
        SyncSetting(settingName);
        
    }
    

    public void SyncSetting(string settingName = "")
    {
        if (settingName == string.Empty)
        {
            SettingText[0].text = $"Notation:\n{NotationNames[Notation]}";
            return;
        }
        switch (settingName)
        {
            case "Notation":
                SettingText[0].text = $"Notation:\n{NotationNames[Notation]}";
                break;
        }
    }

    public void NavigateSettings(string location)
    {
        foreach (var panel in SettingPanels)
            panel.SetActive(false);

        switch (location)
        {
            case "Save":
                SettingPanels[0].SetActive(true);
                break;
            case "Main":
                SettingPanels[1].SetActive(true);
                break;
                
        }
    }
}
