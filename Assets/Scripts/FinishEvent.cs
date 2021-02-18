using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishEvent : MonoBehaviour
{
    private CarController carController;
    private GameObject finishPanel;
    [SerializeField]
    private Text timeText;
    private bool canMove=true;

    private void Start() {
        carController = GameObject.FindGameObjectWithTag("Car").GetComponent<CarController>();
        finishPanel = GameObject.Find("Canvas/finishPanel");
        finishPanel.SetActive(false);
    }
    private void Update() {
        if(!canMove){
            carController.handbrake = true;
        }else{
            return;
        }
    }
    public void onFinish(){
        canMove = false;
        StartCoroutine(finishStage());
    }
    IEnumerator finishStage(){
        int counter = 2;
        while(counter > 0){
            yield return new WaitForSeconds(1);
            counter--;
        }
        finishPanel.SetActive(true);
    }
    public void setTimeText(string time){
        timeText.text = "Time: " + time;
    }
}
