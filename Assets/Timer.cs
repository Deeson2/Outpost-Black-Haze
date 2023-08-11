using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    bool timerActive = false;
    float bestTime;
    public Color red;
    public Color green;

    [Header("Component")]
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI bestTimeText;
    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown = false;
    [Header("Limit Settings")]
    public bool hasLimit;
    public float timerLimit;
    [Header("Format Settings")]
    public bool hasFormat;
    public TimerFormats format;
    private Dictionary<TimerFormats, string> timeFormats = new Dictionary<TimerFormats, string>();

    public static Timer Instance {get; private set;}

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
       timeFormats.Add(TimerFormats.Whole, "0");
       timeFormats.Add(TimerFormats.TenthDecimal, "0.0");
       timeFormats.Add(TimerFormats.HundrethDecimal, "0.00");
       float time = PlayerPrefs.GetFloat("Best Time", 9999999999);
       bestTimeText.text = hasFormat ? time.ToString(timeFormats[format]) : time.ToString();
       bestTime = PlayerPrefs.GetFloat("Best Time", 9999999999);
    }

  
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            ResetBestTime();
        }

        if(timerActive == false){return;}
        currentTime = countDown ? currentTime -=Time.deltaTime : currentTime += Time.deltaTime;

        if(hasLimit && ((countDown && currentTime <= timerLimit) || (!countDown && currentTime>= timerLimit)))
        {
            currentTime = timerLimit;
            SetTimerText();
            timerText.color = red;
            enabled = false;
        }

        if(currentTime < bestTime)
        {
            timerText.color = green;
        }

        if(currentTime > bestTime)
        {
            timerText.color = red;
        }

        SetTimerText();
    }

    public void StartTimer()
    {
        bestTime = PlayerPrefs.GetFloat("Best Time", 9999999999);
        currentTime = 0;
        timerActive = true;
    }

    public void EndTimer()
    {
        timerActive = false;
        SetBestTime(currentTime);
    }

    public void ResetTimer()
    {
        timerActive = false;
        timerText.color = Color.white;
    }
    
    public void ResetBestTime()
    {
        PlayerPrefs.SetFloat("Best Time", 9999999999);
    }

    void SetBestTime(float time)
    {
        if(time < PlayerPrefs.GetFloat("Best Time", 9999999999))
        {
            bestTimeText.text = hasFormat ? time.ToString(timeFormats[format]) : time.ToString();
            PlayerPrefs.SetFloat("Best Time", time);
        }
    }

    private void SetTimerText()
    {
        timerText.text = hasFormat ? currentTime.ToString(timeFormats[format]) : currentTime.ToString();
    }

    private void SetBestTimeText(float value)
    {
        bestTimeText.text = hasFormat ? value.ToString(timeFormats[format]) : value.ToString();
    }
}

public enum TimerFormats
{
    Whole,
    TenthDecimal,
    HundrethDecimal,
}