using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    public MainControls mainControls;
    public GameObject pausePanel;
    public bool isPaused = false;
    private void Awake() {
        mainControls = new MainControls();
        
        mainControls.Car.PauseButton.performed += ctx => pauseMenu();
    }
    private void OnEnable() {
        mainControls.Enable();
    }
    private void OnDisable() {
        mainControls.Disable();
    }
    private void pauseMenu(){
        if(!isPaused){
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            isPaused = true;
        }else{
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
            isPaused = false;
        }
    }
}
