using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void onPlay(){
        SceneManager.LoadScene(PlayerPrefs.GetString("stageSceneName"), LoadSceneMode.Single);
    }
    public void returnToMenu(){
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
