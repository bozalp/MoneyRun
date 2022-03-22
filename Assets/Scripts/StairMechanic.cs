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
    #endregion
    #region Methods
    private void Start()
    {
        DOTween.Init();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextSpawn && moneyStack.counter != 0)
        {
            playerDown = true;
            nextSpawn = Time.time + spawnRate;
            SpawnMoneyStairs();
            AnimationManager.instance.StartWalkAnimation();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) || moneyStack.counter == 0 && player.transform.position.y > .5f)
        {
            DOTween.Kill(moveTween);
            playerDown = true;
        }
        if(playerDown)
        {
            _nextPosition = new Vector3(0, player.transform.localPosition.y - 7f * Time.deltaTime, player.transform.localPosition.z);
            player.transform.localPosition = _nextPosition;
        }
    }
    private void SpawnMoneyStairs()
    {
        if(moneyStack.counter != 0)
        {
            _nextPosition = new Vector3(0, player.transform.localPosition.y + .8f, player.transform.localPosition.z);
            moveTween = player.transform.DOLocalMove(_nextPosition, .5f);
            Destroy(moneyStack.moneyPositions[moneyStack.counter - 1].GetChild(0).gameObject);
            moneyStack.counter--;
            GameObject spawnedMoney = Instantiate(money, transform.position, money.transform.rotation);
            Destroy(spawnedMoney, 2f);
        }
    }
    #endregion
}
