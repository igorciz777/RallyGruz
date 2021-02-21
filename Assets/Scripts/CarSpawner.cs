using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    private Transform spawnPoint;

    private void Awake() {
        spawnPoint = this.transform;
        GameObject.Instantiate(Resources.Load(PlayerPrefs.GetString("carPrefabName")),spawnPoint.position,spawnPoint.transform.rotation);
    }
    public void spawnCar(){
        
    }
}
