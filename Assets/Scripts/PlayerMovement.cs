using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private float forwardSpeed;
    #endregion
    #region Properties
    public float Speed
    {
        get
        {
            return forwardSpeed;
        }
        set
        {
            forwardSpeed = value;
        }
    }
    #endregion
    #region Methods
    void Update()
    {
        if(GameManager.instance.IsStart)
            transform.position += new Vector3(0, 0, Speed * Time.deltaTime);   
    }
    #endregion
}
