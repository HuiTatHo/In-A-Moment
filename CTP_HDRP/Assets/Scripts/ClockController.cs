using UnityEngine;
using TMPro;
using System;

public class ClockController : MonoBehaviour
{
    public TMP_Text climateClockText; // 气候时钟的TextMeshPro组件
    public TMP_Text windowsClockText; // Windows时间的TextMeshPro组件

    // 在Unity编辑器中设置气候时钟剩余时间的年、天、小时、分钟和秒数
    public int remainingYears = 5;
    public int remainingDays = 73;
    public int remainingHours = 22;
    public int remainingMinutes = 3;
    public int remainingSeconds = 36;

    private DateTime targetTime;

    private void Start()
    {
        // 获取当前系统时间
        DateTime currentTime = DateTime.Now;

        // 计算目标时间（当前时间加上剩余时间）
        targetTime = currentTime.AddYears(remainingYears)
                                .AddDays(remainingDays)
                                .AddHours(remainingHours)
                                .AddMinutes(remainingMinutes)
                                .AddSeconds(remainingSeconds);
    }

    private void Update()
    {
        // 获取当前系统时间
        DateTime currentTime = DateTime.Now;

        // 计算剩余时间
        TimeSpan remainingTime = targetTime - currentTime;

        // 将剩余时间转换为气候时钟的字符串形式（例：5yrs 73d 22:03:36）
        string climateClockTimeString = string.Format("{0}yrs {1}d {2:D2}:{3:D2}:{4:D2}",
            remainingTime.Days / 365,
            remainingTime.Days % 365,
            remainingTime.Hours,
            remainingTime.Minutes,
            remainingTime.Seconds);

        // 更新气候时钟的文本显示
        climateClockText.text = climateClockTimeString;

        // 获取当前系统时间的字符串形式（例：yyyy-MM-dd HH:mm:ss）
        string windowsClockTimeString = currentTime.ToString("Now: yyyy-MM-dd HH:mm:ss");

        // 更新Windows时间的文本显示
        windowsClockText.text = windowsClockTimeString;
    }
}