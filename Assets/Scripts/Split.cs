using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : MonoBehaviour
{
    [SerializeField]
    private StageTimer stageTimer;
    [SerializeField]
    private FinishEvent finishEvent;
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
            float time = stageTimer.getTimeInMilliseconds();
            string timestring = stageTimer.getTime();
            float bestTime = PlayerPrefs.GetFloat(stageTimer.stageStringKey + ".bestTimeInMs", 34400000f);
            stageTimer.updateSplitUI();
            stageTimer.updateSplitDifference(splitNum);
            if(stageTimer.canWin){
                PlayerPrefs.SetInt(stageTimer.stageStringKey + ".isWon", 1);
            }
            if(time < bestTime){
                PlayerPrefs.SetFloat(stageTimer.stageStringKey + ".bestTimeInMs", time);
                PlayerPrefs.SetString(stageTimer.stageStringKey + ".stageBestTime", timestring);
            }
            finishEvent.onFinish();
            finishEvent.setTimeText(timestring);
        }
        else
        {
            stageTimer.updateSplitUI();
            stageTimer.updateSplitDifference(splitNum);
        }
    }
    private void convertTime(float time){
    }
}
