using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo : MonoBehaviour
{
    public string stageName, stageLocation, stageLength, stageSurface, stageBeaten, stageTimeToBeat, stageBestTime, stageStringKey, unlockRequirement;
    public int stageWon, unlocked;
    public bool preUnlocked=false;

    private void Awake() {
        if(preUnlocked){
            PlayerPrefs.SetInt(stageStringKey + ".unlocked", 1);
        }
        loadInfo();
        if(stageWon == 1){
            stageBeaten = "Yes";
        }else{
            stageBeaten = "No";
        }
    }
    public void loadInfo(){
        stageBestTime = PlayerPrefs.GetString(stageStringKey + ".stageBestTime", "None");
        stageWon = PlayerPrefs.GetInt(stageStringKey + ".isWon", 0);
        unlocked = PlayerPrefs.GetInt(stageStringKey + ".unlocked", 0);
    }
}
