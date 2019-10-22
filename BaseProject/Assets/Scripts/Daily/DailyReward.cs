using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{
    //UI
    //public Text timeLabel; //only use if your timer uses a label
    //public Button timerButton; //used to disable button when needed
    //public Image _progress;
    //TIME ELEMENTS
    public int hours; //to set the hours
    public int minutes; //to set the minutes
    public int seconds; //to set the seconds
    private bool _timerComplete = false;
    private bool _timerIsReady;
    private TimeSpan _startTime;
    private TimeSpan _endTime;
    private float _remainingTime;
    private TimeSpan _endFinal;
    //progress filler
    private float _value = 1f;
    //reward to claim
    //public bool check;

    //update the time information with what we got some the internet
    private void updateTime()
    {
        if (PlayerPrefs.GetString("_timer") == "Standby")
        {
            PlayerPrefs.SetString("_timer", TimeManager.sharedInstance.getCurrentTimeNow());
            PlayerPrefs.SetInt("_date", TimeManager.sharedInstance.getCurrentDateNow());
        }
        else if (PlayerPrefs.GetString("_timer") != "" && PlayerPrefs.GetString("_timer") != "Standby")
        {
            // PlayerPrefs.SetString("FirstTimeDailyReward","done");
            int _old = PlayerPrefs.GetInt("_date");
            int _now = TimeManager.sharedInstance.getCurrentDateNow();
            //check if a day as passed
            if (_now > _old)
            {//day as passed

                if (DailyManager.instance != null && DailyManager.instance.NextReward())
                {
                    rewardClicked();
                    DailyManager.instance.dailyPanel.SaveData();
                }
                return;
            }
            else if (_now == _old)
            {//same day

                _configTimerSettings();
                return;
            }
            else
            {

                return;
            }
        }

        _configTimerSettings();
    }

    //setting up and configureing the values
    //update the time information with what we got some the internet
    private void _configTimerSettings()
    {
        TimeSpan.TryParse(TimeManager.sharedInstance.getCurrentTimeNow(), out _startTime);
        TimeSpan.TryParse(hours + ":" + minutes + ":" + seconds, out _endTime);
        _endFinal = _endTime.Subtract(_startTime);

        //start timmer where we left off
        setProgressWhereWeLeftOff();
        if ((_endFinal.TotalSeconds + 1) <= 0)
        {
            _timerComplete = true;
            if (DailyManager.instance != null && DailyManager.instance.NextReward())
            {
                rewardClicked();
                DailyManager.instance.dailyPanel.SaveData();
            }
        }
        else
        {
            _timerComplete = false;
            _timerIsReady = true;
            if (DailyManager.instance != null)
            {
                DailyManager.instance.WaitForTimeToOpenReward();
                DailyManager.instance.dailyPanel.SaveData();
            }
        }
    }

    //initializing the value of the timer
    private void setProgressWhereWeLeftOff()
    {
        _value = (float)_endFinal.TotalSeconds / 86400;
       // _progress.fillAmount = _value;
    }



    public IEnumerator CheckTime()
    {
        // disableButton();
       // timeLabel.text = "Checking the time";

        yield return StartCoroutine(TimeManager.sharedInstance.getTime());
        updateTime();


    }
    public void rewardClicked()
    {

        PlayerPrefs.SetString("_timer", "Standby");
        StartCoroutine(CheckTime());
    }


    //update method to make the progress tick
    void Update()
    {
        if (_timerIsReady)
        {
            if (!_timerComplete && PlayerPrefs.GetString("_timer") != "")
            {
                //_value -= Time.deltaTime * 1f / (float)_endTime.TotalSeconds;
                _value -= Time.deltaTime * 1f / (float)_endFinal.TotalSeconds;
               // _progress.fillAmount = _value;

                //this is called once only
                if (_value <= 0 && !_timerComplete)
                {
                    //when the timer hits 0, let do a quick validation to make sure no speed hacks.
                    validateTime();
                    _timerComplete = true;
                }
            }
        }
    }
    public void SetupTime()
    {
        TimeSpan.TryParse(hours + ":" + minutes + ":" + seconds, out _endTime);
        //start timmer where we left off
        setProgressWhereWeLeftOff();
        PlayerPrefs.GetString("FirstTimeDailyReward");
    }

    //validator
    private void validateTime()
    {

        StartCoroutine(CheckTime());
    }


    private void claimReward(int x)
    {

    }
    public void _checktime()
    {
        StartCoroutine(CheckTime());
    }
    public void Add1Day()
    {
        PlayerPrefs.SetInt("_date", TimeManager.sharedInstance.getCurrentDateNow() - 1);

        validateTime();

    }
}
