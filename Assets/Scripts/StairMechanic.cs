using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StairMechanic : MonoBehaviour
{
    [SerializeField]
    private GameObject money;
    [SerializeField]
    private float spawnRate;
    private float nextSpawn = 0f;
    [SerializeField]
    private GameObject player;
    private Vector3 _nextPosition;
    [SerializeField]
    private MoneyStack moneyStack;
    private bool playerDown;
    private TweenCallback callback;
    private Tween moveTween;

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
            _nextPosition = new Vector3(0, player.transform.localPosition.y - 5f * Time.deltaTime, player.transform.localPosition.z);
            player.transform.localPosition = _nextPosition;
        }
        if (player.transform.localPosition.y <= 0)
            player.transform.localPosition = new Vector3(0, 0, player.transform.localPosition.z);
    }
    private void SpawnMoneyStairs()
    {
        if(moneyStack.counter != 0)
        {
            _nextPosition = new Vector3(0, player.transform.localPosition.y + .8f, player.transform.localPosition.z);
            moveTween = player.transform.DOLocalMove(_nextPosition, .5f);
            
            //player.transform.localPosition = _nextPosition;

            Destroy(moneyStack.moneyPositions[moneyStack.counter - 1].GetChild(0).gameObject);
            moneyStack.counter--;
            //moneyStack.moneyPositions[i].GetChild(0).position = transform.position;
            GameObject spawnedMoney = Instantiate(money, transform.position, money.transform.rotation);
            Destroy(spawnedMoney, 2f);
        }
    }
}
