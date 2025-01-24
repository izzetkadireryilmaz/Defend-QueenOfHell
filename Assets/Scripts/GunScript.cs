using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    bool CanIShoot = true, CanIReload = true, CanIChange = true;
    Animator animator;
    float CShootCountdown;
    public Camera MyCam;
    // public GameObject BulletCasing, BulletCasingPoint;
    public GameObject FPSRifle, FPSHandgun, FPSRiflePanel, FPSHandgunPanel;
    public float ShootCountdown, RifleDamage, HandgunDamage;
    public AudioSource ShootSound, MagazineSound;
    public ParticleSystem ShootEffect, BulletEffect, BloodEffect;
    public int Bullets, MagazineCapacity, rBullet, Handgun_Bullets, Handgun_MagazineCapacity, Handgun_rBullet;
    public Text BulletText, rBulletText, Handgun_BulletText, Handgun_rBulletText;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GunChange(FPSRifle, FPSRiflePanel, FPSHandgun, FPSHandgunPanel);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && CanIShoot && Time.time > CShootCountdown)
        {
            if (FPSRifle.activeSelf && Bullets > 0)
            {
                Shoot();
                CShootCountdown = Time.time + ShootCountdown;
            }
            else if (FPSHandgun.activeSelf && Handgun_Bullets > 0)
            {
                Shoot();
                CShootCountdown = Time.time + ShootCountdown;
            }

        }
        if (Input.GetKey(KeyCode.R) && CanIReload == true)
        {
            if (FPSRifle.activeSelf && MagazineCapacity != Bullets && rBullet > 0)
            {
                StartCoroutine(ReloadSystem());
            }
            if (FPSHandgun.activeSelf && Handgun_MagazineCapacity != Handgun_Bullets && Handgun_rBullet > 0)
            {
                StartCoroutine(ReloadSystem());
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeBullets();
        }
        if (Input.GetKey(KeyCode.Alpha1) && CanIChange == true)
        {
            GunChange(FPSRifle, FPSRiflePanel, FPSHandgun, FPSHandgunPanel);
        }
        if (Input.GetKey(KeyCode.Alpha2) && CanIChange == true)
        {
            GunChange(FPSHandgun, FPSHandgunPanel, FPSRifle, FPSRiflePanel);
        }
    }

    IEnumerator ReloadSystem()
    {
        CanIShoot = false;
        CanIReload = false;
        CanIChange = false;
        animator.Play("MagazineAnim");
        if (FPSRifle.activeSelf)
        {
            yield return new WaitForSeconds(2.1f);
            if (rBullet >= MagazineCapacity - Bullets)
            {
                rBullet -= MagazineCapacity - Bullets;
                Bullets = MagazineCapacity;
                rBulletText.text = rBullet.ToString();
                BulletText.text = Bullets.ToString();
            }
            else if (rBullet < MagazineCapacity - Bullets)
            {
                Bullets += rBullet;
                rBullet = 0;
                rBulletText.text = rBullet.ToString();
                BulletText.text = Bullets.ToString();
            }
        }
        if (FPSHandgun.activeSelf)
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
    }
    void Magazine()
    {
        MagazineSound.Play();
    }
    void CanI()
    {
        CanIShoot = true;
        CanIReload = true;
        CanIChange = true;
    }
    void CanIFalse()
    {
        CanIShoot = false;
        CanIReload = false;
        CanIChange = false;
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
        if (FPSRifle.activeSelf)
        {
            Bullets--;
            BulletText.text = Bullets.ToString();
        }
        if (FPSHandgun.activeSelf)
        {
            Handgun_Bullets--;
            Handgun_BulletText.text = Handgun_Bullets.ToString();
        }


        if (Physics.Raycast(MyCam.transform.position, MyCam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                Instantiate(BloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                if (FPSRifle.activeSelf)
                {
                    hit.transform.gameObject.GetComponent<EnemyScript>().Damage(RifleDamage);
                }
                else if (FPSHandgun.activeSelf)
                {
                    hit.transform.gameObject.GetComponent<EnemyScript>().Damage(HandgunDamage);
                }
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
                rBullet += bulletAmount;
                rBulletText.text = rBullet.ToString();
                break;

            case "Handgun":
                Handgun_rBullet += bulletAmount;
                Handgun_rBulletText.text = Handgun_rBullet.ToString();
                break;
        }

    }

    void GunChange(GameObject TrueGunType, GameObject TrueGunTypePanel, GameObject FalseGunType, GameObject FalseGunTypePanel)
    {
        TrueGunType.SetActive(true);
        TrueGunTypePanel.SetActive(true);
        FalseGunType.SetActive(false);
        FalseGunTypePanel.SetActive(false);
        animator.Play("StartAnim");
    }

}
