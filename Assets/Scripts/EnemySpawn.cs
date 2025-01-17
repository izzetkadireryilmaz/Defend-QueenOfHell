using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject[] enemys;
    public GameObject[] enemySpawner;
    public GameObject Player;
    void Start()
    {
        StartCoroutine(EnemySpawner());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator EnemySpawner()
    {
        

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 1.5f));
            int enemy = Random.Range(0, enemys.Length);
            int Spawner = Random.Range(0, enemySpawner.Length);

            GameObject Enemy = Instantiate(enemys[enemy], enemySpawner[Spawner].transform.position, Quaternion.identity);
            Enemy.GetComponent<EnemyScript>().SetaTarget(Player);

        }

    }
}
