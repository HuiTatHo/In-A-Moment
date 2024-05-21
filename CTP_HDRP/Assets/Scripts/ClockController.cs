using UnityEngine;
using TMPro;
using System;

public class ClockController : MonoBehaviour
{
    public TMP_Text climateClockText;
    public TMP_Text windowsClockText;


    public int remainingYears = 5;
    public int remainingDays = 73;
    public int remainingHours = 22;
    public int remainingMinutes = 3;
    public int remainingSeconds = 36;

    private DateTime targetTime;

    private void Start()
    {
        DateTime currentTime = DateTime.Now;

        targetTime = currentTime.AddYears(remainingYears)
                                .AddDays(remainingDays)
                                .AddHours(remainingHours)
                                .AddMinutes(remainingMinutes)
                                .AddSeconds(remainingSeconds);
    }

    private void Update()
    {
        DateTime currentTime = DateTime.Now;

        TimeSpan remainingTime = targetTime - currentTime;

        string climateClockTimeString = string.Format("{0}yrs {1}d {2:D2}:{3:D2}:{4:D2}",
            remainingTime.Days / 365,
            remainingTime.Days % 365,
            remainingTime.Hours,
            remainingTime.Minutes,
            remainingTime.Seconds);

        
        climateClockText.text = climateClockTimeString;

        
        string windowsClockTimeString = currentTime.ToString("Now: yyyy-MM-dd HH:mm:ss");

        
        windowsClockText.text = windowsClockTimeString;
    }
}