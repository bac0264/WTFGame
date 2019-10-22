/// <summary>
/// Created by Leaton Mitchell 11/17/2017
/// Leatonm.net
/// </summary>
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class TimeManager : MonoBehaviour
{

    public static TimeManager sharedInstance = null;
    private string _url = "https://www.microsoft.com/"; //change this to your own
    private string _timeData;
    private string _currentTime;
    private string _currentDate;
    private string _typeoftime;
    public DateTime datetime;

    //make sure there is only one instance of this always.
    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else if (sharedInstance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        if (datetime == null)
        {
            datetime = new DateTime(1, 1, 1, 0, 0, 0);
        }
    }
    public DateTime GetNetTime()
    {
        var myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
        var response = myHttpWebRequest.GetResponse();
        string todaysDates = response.Headers["date"];
        return DateTime.ParseExact(todaysDates,
                                   "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                                   CultureInfo.InvariantCulture.DateTimeFormat,
                                   DateTimeStyles.AssumeUniversal);
    }
    public IEnumerator getTime()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://www.microsoft.com");
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            if (www.isHttpError)
            {
                www = UnityWebRequest.Get("https://www.google.com");
                yield return www.SendWebRequest();
                if (www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    string date = www.GetResponseHeader("date");
                    yield return new WaitForSecondsRealtime(0.1f);
                    datetime = DateTime.ParseExact(date,
                                               "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                                               CultureInfo.InvariantCulture.DateTimeFormat,
                                               DateTimeStyles.AssumeUniversal);
                    Debug.Log(datetime);
                    string[] words_ = datetime.ToString().Split(' ');

                    //timerTestLabel.text = www.text;

                    //setting current time
                    _currentDate = words_[0];
                    _currentTime = words_[1];
                }
            }
            else
            {
                string date = www.GetResponseHeader("date");
                yield return new WaitForSecondsRealtime(0.1f);
                datetime = DateTime.ParseExact(date,
                                           "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                                           CultureInfo.InvariantCulture.DateTimeFormat,
                                           DateTimeStyles.AssumeUniversal);
                string[] _words = datetime.ToString().Split(' ');

                //timerTestLabel.text = www.text;

                //setting current time
                _currentDate = _words[0];
                _currentTime = _words[1];
            }
        }
    }

    //get the current time at startup
    void Start()
    {
        StartCoroutine(getTime());
    }

    //get the current date - also converting from string to int.
    //where 12-4-2017 is 1242017
    public int getCurrentDateNow()
    {
        string[] words = _currentDate.Split('/');
        int x = int.Parse(words[0] + words[1] + words[2]);
        return x;
    }


    //get the current Time
    public string getCurrentTimeNow()
    {
        return _currentTime;
    }
}
