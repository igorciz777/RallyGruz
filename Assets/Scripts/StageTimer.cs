using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class StageTimer : MonoBehaviour
{
    public string timer;
    private Stopwatch stopWatch;
    private StageTimeTable stageTimes;
    private Text timerText, splitText, splitDiffText;
    public bool canWin = false;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GameObject.Find("Canvas/TimeTable/Timer").GetComponent<Text>();
        splitText = GameObject.Find("Canvas/TimeTable/Split").GetComponent<Text>();
        splitDiffText = GameObject.Find("Canvas/TimeTable/SplitDifference").GetComponent<Text>();
        stageTimes = GameObject.Find("Timer").GetComponent<StageTimeTable>();
        stopWatch = new Stopwatch();
    }

    // Update is called once per frame
    void Update()
    {
        updateTimerUI(); 
    }
    public void startTimer(){
        stopWatch.Start();
    }
    public void stopTimer(){
        stopWatch.Stop();
    }
    public void updateSplitUI(){
        splitText.text = string.Format("{0:00}:{1:00}.{2:00}",
            stopWatch.Elapsed.Minutes, stopWatch.Elapsed.Seconds,
            stopWatch.Elapsed.Milliseconds / 10);
    }
    public void updateTimerUI(){
        timerText.text = string.Format("{0:00}:{1:00}.{2:00}",
            stopWatch.Elapsed.Minutes, stopWatch.Elapsed.Seconds,
            stopWatch.Elapsed.Milliseconds / 10);
    }
    public void updateSplitDifference(int splitNum){
        float time = stopWatch.ElapsedMilliseconds;
        float diff = time - (stageTimes.timeTable[splitNum] * 1000);
        if(diff > 0){
            splitDiffText.color = Color.red;
            splitDiffText.text = string.Format("+{0}",diff / 1000);
            canWin = false;
        }else{
            splitDiffText.color = Color.green;
            splitDiffText.text = string.Format("{0}",diff / 1000);
            canWin = true;
        }
    }
}
