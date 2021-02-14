using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelector : MonoBehaviour
{
    public string[] stageNames;
    public string[] sceneNames;
    public Dropdown stageSelector;

    private void Awake() {
        stageSelector.ClearOptions();

        List<string> options = new List<string>();
        foreach(string s in stageNames){
            options.Add(s);
        }
        stageSelector.AddOptions(options);
    }
    public void sendInfoOnPlay(){
        PlayerPrefs.SetString("stageSceneName", sceneNames[stageSelector.value]);
        Debug.Log("Scene name set to " + PlayerPrefs.GetString("stageSceneName"));
    }
}
