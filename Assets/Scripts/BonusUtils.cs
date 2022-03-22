using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusUtils : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private bool addBonus;
    #endregion
    #region Properties
    public bool AddBonus
    {
        get
        {
            return addBonus;
        }
    }
    #endregion
}
