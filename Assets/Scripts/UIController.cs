using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject startScreen;

    private void Update()
    {
        if(!GameManager.instance.IsStart && Input.GetKey(KeyCode.Mouse0))
        {
            GameManager.instance.IsStart = true;
            StartScreenVisible();
            AnimationManager.instance.StartWalkAnimation();
        }    
    }

    public void StartScreenVisible()
    {
        startScreen.SetActive(false);
    }
}
