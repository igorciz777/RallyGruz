using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelector : MonoBehaviour
{
    public string[] sceneNames;
    public GameObject[] sceneDataObjects;
    public Text stageDataText, unlockReq;
    public Button playButton;
    public GameObject lockedPanel;
    public int currentStage;
    [SerializeField]
    private CarSelector carSelector;

    private void Awake() {
        currentStage = getStageChoice();
    }
    private void Start() {
        foreach (GameObject stageObj in sceneDataObjects)
        {
            stageObj.SetActive(false);
            if (sceneDataObjects[currentStage] == stageObj)
            {
                stageObj.SetActive(true);
            }
        }
        updateText();
        checkIfLocked();
    }
    public void nextStage()
    {
        currentStage++;
        if (currentStage >= sceneDataObjects.Length)
        {
            currentStage = 0;
        }
        updateText();
        checkIfLocked();
        foreach (GameObject stageObj in sceneDataObjects)
        {
            stageObj.SetActive(false);
            if (sceneDataObjects[currentStage] == stageObj)
            {
                stageObj.GetComponent<StageInfo>().loadInfo();
                checkIfLocked();
                stageObj.SetActive(true);
            }
        }
    }
    public void previousStage()
    {
        currentStage--;
        if (currentStage < 0)
        {
            currentStage = sceneDataObjects.Length - 1;
        }
        updateText();
        foreach (GameObject stageObj in sceneDataObjects)
        {
            stageObj.SetActive(false);
            if (sceneDataObjects[currentStage] == stageObj)
            {
                stageObj.GetComponent<StageInfo>().loadInfo();
                checkIfLocked();
                stageObj.SetActive(true);
            }
        }
    }
    public void sendInfoOnPlay(){
        PlayerPrefs.SetString("stageSceneName", sceneNames[currentStage]);
        Debug.Log("Scene name set to " + PlayerPrefs.GetString("stageSceneName"));
    }
    public void updateText(){
        StageInfo stageInf = sceneDataObjects[currentStage].GetComponent<StageInfo>();
        string s = string.Format("Name: {0}\nLocation: {1}\nLength: {2}\nSurface: {3}\n\nTime required to win: {4}\nStage beaten: {5}\nBest time: {6}",stageInf.stageName,stageInf.stageLocation,stageInf.stageLength,stageInf.stageSurface,stageInf.stageTimeToBeat,stageInf.stageBeaten,stageInf.stageBestTime);
        stageDataText.text = s;
    }
    public void checkIfLocked(){
        CarInfo currentCarInfo = carSelector.cars[carSelector.currentCar].GetComponent<CarInfo>();
        StageInfo stageInf = sceneDataObjects[currentStage].GetComponent<StageInfo>();
        if(stageInf.unlocked == 0){
            playButton.interactable = false;
            lockedPanel.SetActive(true);
            unlockReq.text = stageInf.unlockRequirement;
        }else if(currentCarInfo.unlocked == 0){
            playButton.interactable = false;
            lockedPanel.SetActive(false);
        }else{
            playButton.interactable = true;
            lockedPanel.SetActive(false);
        }
    }
    public void rememberStageChoice(){
        PlayerPrefs.SetInt("currentStage", currentStage);
    }
    public int getStageChoice(){
        return PlayerPrefs.GetInt("currentStage", 0);
    }
}
