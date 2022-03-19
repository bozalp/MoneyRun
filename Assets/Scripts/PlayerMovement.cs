using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float forwardSpeed;
    void Update()
    {
        if(GameManager.instance.IsStart)
            transform.position += new Vector3(0, 0, forwardSpeed * Time.deltaTime);   
    }
}
