using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSelector : MonoBehaviour
{
    public GameObject[] cars;
    public Text carDataText;
    private int currentCar;
    private CarInfo currentCarInfo;

    // Update is called once per frame
    private void Awake()
    {
        UpdateText();
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
}
