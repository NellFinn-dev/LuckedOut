using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatSO : ScriptableObject
{
    #region instance variables
    [SerializeField]
    private float _Time;

    [SerializeField]
    private float _BestTime;

    [SerializeField]
    private float _Combo;

    [SerializeField]
    private float _BestCombo;
    #endregion

    #region methods

    public float Time
    {
        get { return _Time; }
        set { _Time = value; }
    }

    public float BestTime
    {
        get { return _BestTime; }
        set { _BestTime = value; }
    }

    public float Combo
    {
        get { return _Combo; }
        set { _Combo = value; }
    }

    public float BestCombo
    {
        get { return _BestCombo; }
        set { _BestCombo = value; }
    }

    #endregion

}
