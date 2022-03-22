using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMoney : MonoBehaviour
{
    #region Fields
    private int moneyCount;
    #endregion

    #region Properties
    public int MoneyCount
    {
        get
        {
            return moneyCount;
        }
    }
    #endregion
    #region Methods
    private void Start()
    {
        moneyCount = GetComponentsInChildren<Transform>().Length - 1;
    }
    #endregion
}
