using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    #region Fields
    public static AnimationManager instance;

    private Animator animator;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private MoneyStack moneyStack;
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
        animator = player.GetComponent<Animator>();
    }
    public void StartWalkAnimation()
    {
        animator.SetLayerWeight(1, 0);
        animator.SetTrigger("Walk");
    }
    public void StartFallingAnimation()
    {
        animator.SetLayerWeight(1, 1);

    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (player.transform.position.y > 1)
            {
                StartFallingAnimation();
            }
        }
        if (player.transform.position.y < 1 && GameManager.instance.IsStart)
        {
            StartWalkAnimation();
        }
        if (moneyStack.counter == 0 && Input.GetKey(KeyCode.Mouse0) && player.transform.position.y > .5f)
        {
            StartFallingAnimation();
        }
    }
    #endregion
}
