using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Diagnostics; // Required for the Stopwatch class
using Debug = UnityEngine.Debug;

public class SpeedRunTimer : MonoBehaviour
{
    #region instance variables
    public float timer;
    public string TimeString;
    public TextMeshProUGUI Text;
    public Entity healthscript;
    [SerializeField]
    private FloatSO _Time;
    public PlayerInputs Controller;
    public bool TimedOut;
    public bool timedLevel;
    public bool isRunning;
    private Stopwatch stopwatch = new Stopwatch();
    #endregion

    #region methods
    private void Start()
    {
        StartStopwatch();
    }

    public void StartStopwatch()
    {
        if (!stopwatch.IsRunning)
        {
            stopwatch.Start();
            isRunning = true;
            Debug.Log("Stopwatch started.");
        }
    }

    public void Update()
    {

        timer = Time.timeSinceLevelLoad;
        TimeString = timer.ToString("f1");

        // Displays the timer on the UI
        if (healthscript.health > 0 && Time.timeSinceLevelLoad < 60 && timedLevel)
        {
            Text.text = TimeString;
            _Time.Time = Time.timeSinceLevelLoad;
        } else if (healthscript.health > 0)
        {
            Text.text = TimeString;
            _Time.Time = Time.timeSinceLevelLoad;
        }
    }

    // Called at the end of the 
    public void Check()
    {
        if(_Time.Time < _Time.BestTime || _Time.BestTime == 0)
        {
            _Time.BestTime = _Time.Time;
        }
    }
    #endregion
}
