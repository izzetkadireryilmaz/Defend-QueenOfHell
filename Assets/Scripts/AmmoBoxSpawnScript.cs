using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxSpawnScript : MonoBehaviour
{
    public List<GameObject> AmmoBoxSpawner = new List<GameObject>();
    public GameObject AmmoBox;
    public static bool BoxisThere;
    void Start()
    {
        BoxisThere = false;
        StartCoroutine(CreateAmmoBox());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator CreateAmmoBox()
    {
        while (true)
        {
            yield return null;
            if (!BoxisThere)
            {
                yield return new WaitForSeconds(Random.Range(5, 10));
                int SpawnIndex = Random.Range(0, 4);
                Instantiate(AmmoBox, AmmoBoxSpawner[SpawnIndex].transform.position, AmmoBoxSpawner[SpawnIndex].transform.rotation);
                BoxisThere = true;
            }

        }


    }
}
