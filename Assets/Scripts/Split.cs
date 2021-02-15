using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : MonoBehaviour
{
    [SerializeField]
    private StageTimer stageTimer;
    public System.TimeSpan currentSplitTime;
    public int splitNum;
    public bool isStart = false, isFinish = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        if (isStart)
        {
            stageTimer.startTimer();
        }
        else if (isFinish)
        {
            stageTimer.stopTimer();
            stageTimer.updateSplitUI();
            stageTimer.updateSplitDifference(splitNum);
        }
        else
        {
            stageTimer.updateSplitUI();
            stageTimer.updateSplitDifference(splitNum);
        }
    }
}
