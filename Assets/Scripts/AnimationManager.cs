using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager instance;
    [SerializeField]
    private Animator animator;
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
    public void StartWalkAnimation()
    {
        animator.SetTrigger("Walk");
    }
    public void StartFallingAnimation()
    {
        animator.SetLayerWeight(1, 1);
    }
}
