using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Fields
    public static GameManager instance;
    private bool _isStart, _isFinish;
    #endregion
    #region Properties
    public bool IsStart
    {
        get
        {
            return _isStart;
        }
        set
        {
            _isStart = value;
        }
    } 
    public bool IsFinish
    {
        get
        {
            return _isFinish;
        }
        set
        {
            _isFinish = value;
        }
    }
    #endregion

    #region Methods
    private void Singleton()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    private void Awake()
    {
        Singleton();
    }
    #endregion
}
