using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartEvent : MonoBehaviour
{
    private CarController carController;
    private Text startText;
    private GameObject startPanel;
    [SerializeField]
    private GameObject startCamera;
    private bool canMove;
    public int counter;

    private void Start() {
        Time.timeScale = 1f;
        carController = GameObject.FindGameObjectWithTag("Car").GetComponent<CarController>();
        carController.enabled = false;
        canMove = false;
        startText = GameObject.Find("Canvas/StartText").GetComponent<Text>();
        startPanel = GameObject.Find("Canvas/startPanel");
        startText.enabled = false;
        
    }
    private void Update() {
        if(!canMove){
            carController.handbrake = true;
        }else{
            return;
        }
    }
    public void StartCountdown(){
        startCamera.SetActive(false);
        startText.enabled = true;
        carController.enabled = true;
        startPanel.SetActive(false);
        StartCoroutine(Countdown());
    }
    IEnumerator Countdown(){
        while(counter > 0){
            yield return new WaitForSeconds(1);
            counter--;
            startText.text = ""+counter;
        }
        canMove = true;
        carController.handbrake = false;
        startText.enabled = false;
    }
}
