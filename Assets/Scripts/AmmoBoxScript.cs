using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxScript : MonoBehaviour
{
    string[] Guns = { "Deagle","Rifle" };
    int[] Bullets = { 5, 10, 15, 20};
    public string select_gun;
    public int select_bullets;
    void Start()
    {
        select_gun = Guns[Random.Range(0, Guns.Length)];
        select_bullets = Bullets[Random.Range(0, Bullets.Length)];

        Debug.Log(select_gun);
        Debug.Log(select_bullets);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
