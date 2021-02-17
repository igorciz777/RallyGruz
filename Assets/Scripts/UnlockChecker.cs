using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockChecker : MonoBehaviour
{
    public int stageIndex=1;

    private void Start() {
        checkStage(stageIndex);
    }
    private void checkStage(int stageIndex){
        switch(stageIndex){
            case 1:
                break;
            case 2:
                int isWon = PlayerPrefs.GetInt("Stage1.isWon", 0);
                if(isWon == 1){
                    PlayerPrefs.SetInt("Stage2.unlocked", 1);
                }
                break;
            default:
                Debug.Log("Unspecified winning condition");
                break;
        }
    }
}
