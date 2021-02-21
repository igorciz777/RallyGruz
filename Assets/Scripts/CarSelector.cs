using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSelector : MonoBehaviour
{
    public GameObject[] cars;
    public Text carDataText;
    public int currentCar;
    private CarInfo currentCarInfo;
    [SerializeField]
    private StageSelector stageSelector;
    [SerializeField]
    private GameObject carLockedText;
    [SerializeField]
    private Text reqToUnlockText;

    // Update is called once per frame
    private void Awake() {
        currentCar = getCarChoice();
        foreach (GameObject carObj in cars)
        {
            carObj.SetActive(false);
            if (cars[currentCar] == carObj)
            {
                carObj.SetActive(true);
            }
        }
    }
    private void Start()
    {
        UpdateText();
    }
    private void Update() {
        checkIfLocked();
    }
    public void nextCar()
    {
        currentCar++;
        if (currentCar >= cars.Length)
        {
            currentCar = 0;
        }
        UpdateText();
        foreach (GameObject carObj in cars)
        {
            carObj.SetActive(false);
            if (cars[currentCar] == carObj)
            {
                carObj.GetComponent<CarInfo>().loadInfo();
                checkIfLocked();
                carObj.SetActive(true);
            }
        }
    }
    public void previousCar()
    {
        currentCar--;
        if (currentCar < 0)
        {
            currentCar = cars.Length - 1;
        }
        UpdateText();
        foreach (GameObject carObj in cars)
        {
            carObj.SetActive(false);
            if (cars[currentCar] == carObj)
            {
                carObj.GetComponent<CarInfo>().loadInfo();
                checkIfLocked();
                carObj.SetActive(true);
            }
        }
    }
    public void UpdateText()
    {
        currentCarInfo = cars[currentCar].GetComponent<CarInfo>();
        string s = string.Format("Name: {0}\nType: {1}\nPower: {2}\nTransmission: {3}\nMax RPM: {4}\nWeight: {5}", currentCarInfo.carName, currentCarInfo.driveType,currentCarInfo.engineHP, currentCarInfo.transmissionCount, currentCarInfo.engineMaxRPM, currentCarInfo.carWeight);
        carDataText.text = s;
    }
    public void sendInfoOnPlay(){
        currentCarInfo = cars[currentCar].GetComponent<CarInfo>();
        PlayerPrefs.SetString("carPrefabName", currentCarInfo.prefabObject.name);
        Debug.Log("Prefab name set to " + PlayerPrefs.GetString("carPrefabName"));
    }
    public void rememberCarChoice(){
        PlayerPrefs.SetInt("currentCar", currentCar);
    }
    public int getCarChoice(){
        return PlayerPrefs.GetInt("currentCar", 0);
    }
    public void checkIfLocked(){
        currentCarInfo = cars[currentCar].GetComponent<CarInfo>();
        StageInfo stageInf = stageSelector.sceneDataObjects[stageSelector.currentStage].GetComponent<StageInfo>();
        if(currentCarInfo.unlocked == 0){
            carLockedText.SetActive(true);
            reqToUnlockText.text = currentCarInfo.reqToUnlock;
            stageSelector.playButton.interactable = false;
        }else if(stageInf.unlocked == 1){
            carLockedText.SetActive(false);
            stageSelector.playButton.interactable = true;
        }
        else{
            carLockedText.SetActive(false);
        }
    }
}
