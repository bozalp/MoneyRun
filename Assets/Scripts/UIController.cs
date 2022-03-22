using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private GameObject startScreen, levelEndScreen;
    #endregion

    #region Methods
    private void Update()
    {
        if(!GameManager.instance.IsStart && Input.GetKey(KeyCode.Mouse0))
        {
            GameManager.instance.IsStart = true;
            StartScreenVisible();
            AnimationManager.instance.StartWalkAnimation();
        }    
    }

    public void StartScreenVisible() => startScreen.SetActive(false);
    public void LevelEndScreenVisible() => levelEndScreen.SetActive(true);
    public void NextLevel() => SceneManager.LoadScene(0);
    #endregion
}
