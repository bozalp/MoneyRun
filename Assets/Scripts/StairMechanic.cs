using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StairMechanic : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private GameObject money;
    [SerializeField]
    private float spawnRate;    
    [SerializeField]
    private GameObject player;    
    [SerializeField]
    private MoneyStack moneyStack;

    private float nextSpawn = 0f;
    private Vector3 _nextPosition;
    private bool playerDown;
    private Tween moveTween;
    private int counter = 1;
    private PlayerMovement playerMovement;
    private UIController UIController;
    #endregion

    #region Methods
    private void Start()
    {
        DOTween.Init();
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        UIController = GameObject.FindObjectOfType<UIController>();
    }

    private void Update()
    {
        if(!GameManager.instance.IsFinish)
        {
            if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextSpawn && moneyStack.counter != 0)
            {
                playerDown = true;
                nextSpawn = Time.time + spawnRate;
                SpawnMoneyStairs(false);
                AnimationManager.instance.StartWalkAnimation();
            }

            if (Input.GetKeyUp(KeyCode.Mouse0) || moneyStack.counter == 0 && player.transform.position.y > .5f)
            {
                DOTween.Kill(moveTween);
                playerDown = true;
            }
            if (playerDown)
            {
                _nextPosition = new Vector3(0, player.transform.localPosition.y - 7f * Time.deltaTime, player.transform.localPosition.z);
                player.transform.localPosition = _nextPosition;
            }
        }
        if(GameManager.instance.IsFinish && Time.time > nextSpawn && moneyStack.counter != 0)
        {
            nextSpawn = Time.time + spawnRate;
            SpawnMoneyStairs(true);
            AnimationManager.instance.StartWalkAnimation();
        }
    }
    private void SpawnMoneyStairs(bool endLevel)
    {
        if(moneyStack.counter != 0)
        {
            
            if(!endLevel)
            {
                AddStair();
            }
            else
            {
                for (int i = 0; i < counter * 2; i++)
                {
                    if(moneyStack.counter != 0)
                    {
                        AddStair();
                    }
                    if (moneyStack.counter <= 0)
                    {
                        moneyStack.counter = 0;
                        playerMovement.Speed = 0;
                        AnimationManager.instance.StartDanceAnimation();
                        UIController.LevelEndScreenVisible();
                        break;
                    }
                }
                counter++;
            }

        }
    }
    private void AddStair()
    {
        _nextPosition = new Vector3(0, player.transform.localPosition.y + .8f, player.transform.localPosition.z);
        moveTween = player.transform.DOLocalMove(_nextPosition, .5f);
        Destroy(moneyStack.moneyPositions[moneyStack.counter - 1].GetChild(0).gameObject);
        moneyStack.counter--;
        GameObject spawnedMoney = Instantiate(money, transform.position, money.transform.rotation);
    }
    #endregion
}
