using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerThingAddup : MonoBehaviour
{
    public bool On;
    public TextMeshProUGUI Timetxt;
    public TextMeshProUGUI BestCombo;
    public TextMeshProUGUI BestTime;
    public float ammount;
    [SerializeField]
    private FloatSO _Time;

    public int timeInt;
    public int bestTimeInt;

    // Displays text for the scoring
    private void OnEnable()
    {
        if(_Time.Time < _Time.BestTime)
        {
            _Time.BestTime = _Time.Time;
        }

        timeInt = (int)_Time.Time;
        bestTimeInt = (int)_Time.BestTime;
        Timetxt.text = timeInt + "s";
        BestCombo.text = "" + _Time.BestCombo;
        BestTime.text = "" + (int)_Time.BestTime + "s";
    }

}
