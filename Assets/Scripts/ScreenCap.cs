using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenCap : MonoBehaviour
{
    [SerializeField]
    public MainControls mainControls;
    public string filename;

    private void Awake() {
        mainControls = new MainControls();
        mainControls.Enable();
        mainControls.Car.TakeScreenshot.performed += ctx => takeScreenshot();
    }
    private void takeScreenshot(){
        Debug.Log("shashin");
        ScreenCapture.CaptureScreenshot(filename,4);
    }
}
