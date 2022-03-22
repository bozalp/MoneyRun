using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Fields
    public static GameManager instance;
    private bool _isStart;
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
