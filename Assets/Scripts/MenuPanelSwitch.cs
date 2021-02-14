using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject mainPanel, playPanel, optionsPanel;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        returnMain();
    }

    public void goOptions(){
        mainPanel.SetActive(false);
        playPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
    public void goPlay(){
        mainPanel.SetActive(false);
        playPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }
    public void returnMain(){
        mainPanel.SetActive(true);
        playPanel.SetActive(false);
        optionsPanel.SetActive(false);
    }
}
