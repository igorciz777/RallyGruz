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
                int isWon1 = PlayerPrefs.GetInt("Stage1.isWon", 0);
                if(isWon1 == 1){
                    PlayerPrefs.SetInt("Stage2.unlocked", 1);
                }
                break;
            case 3:
                int isWon2 = PlayerPrefs.GetInt("Stage2.isWon", 0);
                if(isWon2 == 1){
                    PlayerPrefs.SetInt("Stage3.unlocked", 1);
                }
                break;
            case 4:
                int isWon3 = PlayerPrefs.GetInt("Stage3.isWon", 0);
                if(isWon3 == 1){
                    PlayerPrefs.SetInt("Stage4.unlocked", 1);
                }
                break;
            case 5:
                int isWon4 = PlayerPrefs.GetInt("Stage4.isWon", 0);
                if(isWon4 == 1){
                    PlayerPrefs.SetInt("Stage5.unlocked", 1);
                }
                break;
            case 6:
                int isWon5 = PlayerPrefs.GetInt("Stage5.isWon", 0);
                if(isWon5 == 1){
                    PlayerPrefs.SetInt("Stage6.unlocked", 1);
                }
                break;
            default:
                Debug.Log("Unspecified winning condition");
                break;
        }
    }
}
