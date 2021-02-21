using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCarCheck : MonoBehaviour
{
    public string carStringKey;

    private void Awake() {
        checkCar(carStringKey);
    }
    private void checkCar(string carStringKey){
        switch(carStringKey){
            case "polonez":
                break;
            case "fiat126p":
                int stage1isWon = PlayerPrefs.GetInt("Stage1.isWon", 0);
                if(stage1isWon == 1){
                    PlayerPrefs.SetInt("fiat126p.unlocked", 1);
                }
                break;
            case "bmw":
                int stage2isWon = PlayerPrefs.GetInt("Stage2.isWon", 0);
                if(stage2isWon == 1){
                    PlayerPrefs.SetInt("bmw.unlocked", 1);
                }
                break;
            case "lada":
                int stage4isWon = PlayerPrefs.GetInt("Stage4.isWon", 0);
                if(stage4isWon == 1){
                    PlayerPrefs.SetInt("lada.unlocked", 1);
                }
                break;
            default:
                Debug.Log("Unspecified winning condition");
                break;
        }
    }
}
