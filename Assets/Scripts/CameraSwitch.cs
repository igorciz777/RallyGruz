using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField]
    public MainControls cameraController;
    [SerializeField]
    private GameObject[] cameraObjects;
    [SerializeField]
    private int currentCamera = 0;
    // Start is called before the first frame update
    void Awake()
    {
        cameraController = this.transform.GetComponent<CarController>().carController;
        
        cameraController.Car.ChangeCamera.performed += ctx => changeCamera();
    }
    // Update is called once per frame
    void changeCamera()
    {
        currentCamera++;
        if(currentCamera >= cameraObjects.Length){
            currentCamera = 0;
        }  
        foreach(GameObject camObj in cameraObjects){
            camObj.SetActive(false);
            if(cameraObjects[currentCamera] == camObj){
                camObj.SetActive(true);
            }
        }
    }
}
