using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private bool _isStart;

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
}
