using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInfo : MonoBehaviour
{
    public string carName, driveType, transmissionCount, carWeight, engineHP, engineMaxRPM, carStringKey, reqToUnlock;
    public GameObject prefabObject;
    public int unlocked;
    public bool preUnlocked=false;
    private void Awake() {
        if(preUnlocked){
            PlayerPrefs.SetInt(carStringKey + ".unlocked", 1);
        }
    }
    private void Start() {
        loadInfo();
    }
    public void loadInfo(){
        unlocked = PlayerPrefs.GetInt(carStringKey + ".unlocked", 0);
    }
}
