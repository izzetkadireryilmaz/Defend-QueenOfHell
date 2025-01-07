using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandgunScript : MonoBehaviour
{
    bool CanIShoot = true;
    bool CanIReload = true;
    Animator animator;
    float CShootCountdown;
    public Camera MyCam;
    //public GameObject BulletCasing, BulletCasingPoint;
    public float ShootCountdown;
    public AudioSource ShootSound, MagazineSound;
    public ParticleSystem ShootEffect, BulletEffect, BloodEffect;
    public int Handgun_Bullets, Handgun_MagazineCapacity, Handgun_rBullet;
    public Text Handgun_BulletText, Handgun_rBulletText;

    // Start is called before the first frame update
    void Start()
    {
        Handgun_rBulletText.text = Handgun_rBullet.ToString();
        Handgun_BulletText.text = Handgun_Bullets.ToString();
        animator = GetComponent<Animator>();
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && CanIShoot && Time.time > CShootCountdown && Handgun_Bullets > 0)
        {
            Shoot();
            CShootCountdown = Time.time + ShootCountdown;
        }
        if (Input.GetKey(KeyCode.R) && CanIReload == true && Handgun_MagazineCapacity != Handgun_Bullets && Handgun_rBullet > 0)
        {
            StartCoroutine(ReloadSystem());
            CanIShoot = false;
            CanIReload = false;
            animator.Play("MagazineAnim");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeBullets();
        }
    }

    IEnumerator ReloadSystem()
    {
        yield return new WaitForSeconds(2.1f);
        if (Handgun_rBullet >= Handgun_MagazineCapacity - Handgun_Bullets)
        {
            Handgun_rBullet -= Handgun_MagazineCapacity - Handgun_Bullets;
            Handgun_Bullets = Handgun_MagazineCapacity;
            Handgun_rBulletText.text = Handgun_rBullet.ToString();
            Handgun_BulletText.text = Handgun_Bullets.ToString();
        }
        else if (Handgun_rBullet < Handgun_MagazineCapacity - Handgun_Bullets)
        {
            Handgun_Bullets += Handgun_rBullet;
            Handgun_rBullet = 0;
            Handgun_rBulletText.text = Handgun_rBullet.ToString();
            Handgun_BulletText.text = Handgun_Bullets.ToString();
        }
    }
    void Magazine()
    {
        MagazineSound.Play();
    }
    void CanI()
    {
        CanIShoot = true;
        CanIReload = true;
    }
    void Shoot()
    {
        //GameObject BulletClone = Instantiate(BulletCasing, BulletCasingPoint.transform.position, BulletCasingPoint.transform.rotation);
        //Rigidbody rb = BulletClone.GetComponent<Rigidbody>();
        //rb.AddRelativeForce(new Vector3 (-10f, 1, 0) * 30);
        RaycastHit hit;

        ShootSound.Play();
        ShootEffect.Play();
        animator.Play("RifleAnim");
        Handgun_Bullets--;
        Handgun_BulletText.text = Handgun_Bullets.ToString();

        if (Physics.Raycast(MyCam.transform.position, MyCam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                Instantiate(BloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else
            {
                Instantiate(BulletEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }


    }
    void TakeBullets()
    {
        RaycastHit hit;

        if (Physics.Raycast(MyCam.transform.position, MyCam.transform.forward, out hit, 4))
        {

            if (hit.transform.gameObject.CompareTag("AmmoBox"))
            {
                SaveBullets(hit.transform.gameObject.GetComponent<AmmoBoxScript>().select_gun, hit.transform.gameObject.GetComponent<AmmoBoxScript>().select_bullets);
                AmmoBoxSpawnScript.BoxisThere = false;
                Destroy(hit.transform.parent.gameObject);
            }

        }
    }
    void SaveBullets(string gunType, int bulletAmount)
    {
        switch (gunType)
        {
            case "Rifle":
                Handgun_rBullet += bulletAmount;
                Handgun_rBulletText.text = Handgun_rBullet.ToString();
                break;

            case "Deagle":

                break;
        }

    }

}
