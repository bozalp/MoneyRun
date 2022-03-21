using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyStack : MonoBehaviour
{
    public int counter;
    public List<Transform> moneyPositions;
    [SerializeField]
    private Transform spawnPosition, spawnParent;
    [SerializeField]
    private GameObject money;
    [SerializeField]
    private GameObject moneyParticleEffect;

    private void Start()
    {
        for (int i = 0; i < 15; i++)
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
            //item.Rotate(new Vector3(-90, 100, 0));
        }
    }

    private void Update()
    {
        if (transform.localPosition.y <= 0 && !GetComponent<Rigidbody>().useGravity)
        {
            transform.localPosition = new Vector3(0, 0, transform.localPosition.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Money"))
        {
            int moneyCount = other.transform.GetComponent<CollectableMoney>().MoneyCount;
            Destroy(other.gameObject);
            for (int i = 0; i < moneyCount; i++)
            {
                GameObject spawnedMoney = Instantiate(money, spawnParent.position, transform.rotation);
                for (int j = moneyPositions.Count - 1; j >= 0; j--)
                {
                    if(moneyPositions[j].childCount == 0)
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
        if(other.transform.CompareTag("Space"))//2 yol arasindaki boslukta gravity aciyorum. Karakter asagi dusebilsin diye.
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Space"))//2 yol arasindaki bosluktan gecince gravity kapatiyorum.
        {
            GetComponent<Rigidbody>().useGravity = false;
        }
    }
}
