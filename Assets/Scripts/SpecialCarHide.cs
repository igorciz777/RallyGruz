using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCarHide : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject carHide;
    private CarInfo carInfo;
    [SerializeField]
    private CarSelector carSelector;
    private string carName, driveType, transmissionCount, carWeight, engineHP, engineMaxRPM;

    private void Awake()
    {
        carInfo = this.GetComponent<CarInfo>();
        carName = carInfo.carName;
        driveType = carInfo.driveType;
        transmissionCount = carInfo.transmissionCount;
        carWeight = carInfo.carWeight;
        engineHP = carInfo.engineHP;
        engineMaxRPM = carInfo.engineMaxRPM;
    }
    private void Update()
    {
        if (carInfo.unlocked == 0)
        {
            carHide.SetActive(true);
            carInfo.carName = carName;
            carInfo.carWeight = carWeight;
            carInfo.driveType = driveType;
            carInfo.transmissionCount = transmissionCount;
            carInfo.engineHP = engineHP;
            carInfo.engineMaxRPM = engineMaxRPM;
            carSelector.UpdateText();
        }
        else
        {
            carHide.SetActive(false);
            carInfo.carName = "Gr.B S4";
            carInfo.carWeight = "~890kg";
            carInfo.driveType = "R4 4WD ";
            carInfo.transmissionCount = "Manual 5 Speed";
            carInfo.engineHP = "500HP @ 8000RPM";
            carInfo.engineMaxRPM = "8500 RPM";
            carSelector.UpdateText();
        }
    }
}
