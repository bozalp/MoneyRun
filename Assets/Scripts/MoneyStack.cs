using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoneyStack : MonoBehaviour
{
    #region Fields
    public int counter;

    public List<Transform> moneyPositions;
    [SerializeField]
    private Transform spawnPosition, spawnParent;
    [SerializeField]
    private GameObject money, moneyParticleEffect;
    [SerializeField]
    private PlayerMovement playerMovement;

    private float _forwardSpeed;
    #endregion
    #region Methods

    private void Start()
    {
        DOTween.Init();
        CreateMoneyPositions();
        _forwardSpeed = playerMovement.Speed;
    }

    private void Update()
    {
        if (transform.localPosition.y <= 0 && !GetComponent<Rigidbody>().useGravity)
        {
            transform.localPosition = new Vector3(0, 0, transform.localPosition.z);
        }
    }
    private void CreateMoneyPositions()
    {
        for (int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Transform pos = Instantiate(spawnPosition, spawnParent.position, spawnPosition.rotation);
                moneyPositions.Add(pos);
                pos.position = new Vector3(spawnPosition.position.x + j * .4f, i * .25f + spawnParent.position.y, spawnParent.position.z);
            }
        }
        foreach (var item in moneyPositions)
        {
            item.SetParent(spawnParent);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Money"))
        {
            AddMoney(other.transform.GetComponent<CollectableMoney>().MoneyCount);
            Destroy(other.gameObject);
        }
        if(other.transform.CompareTag("Obstacle"))
        {
            DOTween.To(() => playerMovement.Speed, _forwardSpeed=> playerMovement.Speed=_forwardSpeed , -15f, 1f).From();
            for (int i = 0; i < 5; i++)
            {
                Destroy(moneyPositions[counter - 1].GetChild(0).gameObject);
                counter--;
            }
        }
        if(other.transform.CompareTag("Gate"))
        {
            if(other.transform.GetComponent<BonusUtils>().AddBonus)
            {
                AddMoney(10);
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    Destroy(moneyPositions[counter - 1].GetChild(0).gameObject);
                    counter--;
                }
            }
        }
        if(other.transform.CompareTag("Finish"))
        {
            GameManager.instance.IsFinish = true;
        }
    }
    private void AddMoney(int moneyCount)
    {
        for (int i = 0; i < moneyCount; i++)
        {
            GameObject spawnedMoney = Instantiate(money, spawnParent.position, transform.rotation);
            for (int j = moneyPositions.Count - 1; j >= 0; j--)
            {
                if (moneyPositions[j].childCount == 0)
                {
                    spawnedMoney.transform.SetParent(moneyPositions[j]);
                    spawnedMoney.transform.localPosition = new Vector3(0, 0, 0);
                    spawnedMoney.transform.eulerAngles = new Vector3(-90, Random.Range(80, 110), 0);
                }
            }
        }

        counter += moneyCount;
        Instantiate(moneyParticleEffect, new Vector3(0, .4f, spawnParent.position.z), transform.rotation);
    }
    #endregion
}
