using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMoney : MonoBehaviour
{
    private int moneyCount;
    private void Start()
    {
        moneyCount = GetComponentsInChildren<Transform>().Length - 1;
    }
    public int MoneyCount
    {
        get
        {
            return moneyCount;
        }
    }
   
}
