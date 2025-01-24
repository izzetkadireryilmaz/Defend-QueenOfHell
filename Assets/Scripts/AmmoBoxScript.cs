using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBoxScript : MonoBehaviour
{
    string[] Guns = { "Rifle", "Handgun" };
    int[] Bullets = { 5, 10, 15, 20 };
    public string select_gun;
    public int select_bullets;
    public List<Sprite> Guns_Img = new List<Sprite>();
    public Image GunImg;


    void Start()
    {
        int SelectIndex = Random.Range(0, Guns.Length);
        select_gun = Guns[SelectIndex];
        select_bullets = Bullets[Random.Range(0, Bullets.Length)];
        GunImg.sprite = Guns_Img[SelectIndex]; 

        Debug.Log(select_gun);
        Debug.Log(select_bullets);

    }
}
