using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyStack : MonoBehaviour
{
    public List<Transform> moneyPositions;
    [SerializeField]
    private Transform spawnPosition, spawnParent;
    [SerializeField]
    private GameObject money;
    public int counter;

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
        }

    }
}
