using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextSpawn && moneyStack.counter != 0)
        {            
            nextSpawn = Time.time + spawnRate;
            SpawnMoneyStairs();
            AnimationManager.instance.StartWalkAnimation();
        }
    }
    private void SpawnMoneyStairs()
    {
        if(moneyStack.counter != 0)
        {
            _nextPosition = new Vector3(0, player.transform.localPosition.y + .5f, player.transform.localPosition.z);
            //player.transform.localPosition = Vector3.MoveTowards(player.transform.localPosition, _nextPosition, 1f);
            player.transform.localPosition = _nextPosition;
            Destroy(moneyStack.moneyPositions[moneyStack.counter - 1].GetChild(0).gameObject);
            moneyStack.counter--;
            //moneyStack.moneyPositions[i].GetChild(0).position = transform.position;
            GameObject spawnedMoney = Instantiate(money, transform.position, money.transform.rotation);
            Destroy(spawnedMoney, 2f);
        }
    }
}
