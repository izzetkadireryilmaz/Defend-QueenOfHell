using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AidKitSpawner : MonoBehaviour
{
    public List<GameObject> AidKitSpawners = new List<GameObject>();
    public GameObject AidKit;
    public static bool KitisThere;
    void Start()
    {
        KitisThere = false;
        StartCoroutine(CreateAidKit());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator CreateAidKit()
    {
        while (true)
        {
            yield return null;
            if (!KitisThere)
            {
                yield return new WaitForSeconds(Random.Range(5, 10));
                int SpawnIndex = Random.Range(0, 4);
                Instantiate(AidKit, AidKitSpawners[SpawnIndex].transform.position, AidKitSpawners[SpawnIndex].transform.rotation);
                KitisThere = true;
            }

        }


    }
}
