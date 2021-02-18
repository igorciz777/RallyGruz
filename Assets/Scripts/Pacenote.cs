using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacenote : MonoBehaviour
{
    [SerializeField]
    private int durationTime;
    [SerializeField]
    private GameObject pacenote;
    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Player")){
            return;
        }
        StartCoroutine(viewPacenote(durationTime));

    }
    private void Awake() {
        pacenote.SetActive(false);
    }
    IEnumerator viewPacenote(int time){
        pacenote.SetActive(true);
        yield return new WaitForSeconds(time);
        pacenote.SetActive(false);
    }
}
