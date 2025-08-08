using System;
using TMPro;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    [SerializeField] TMP_Text stopwatchText;
    bool isRunning = true;

    void OnEnable()
    {
        Enemy.OnEnemyHitPlayer += StopStopwatch;
    }
    void OnDisable()
    {
        Enemy.OnEnemyHitPlayer -= StopStopwatch;
    }

    void Update()
    {
        if (!isRunning) return;
        stopwatchText.text = Time.timeSinceLevelLoad.ToString("F0");
    }
    
    public void StopStopwatch()
    {
        isRunning = false;
    }
}
