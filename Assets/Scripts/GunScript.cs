using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public bool CanIShoot;
    float CShootCountdown;
    public float ShootCountdown; 
    public Camera MyCam;
    public AudioSource ShootSound;
    public ParticleSystem ShootEffect;
    public ParticleSystem BulletEffect;
    public ParticleSystem BloodEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && CanIShoot && Time.time > CShootCountdown)
        {
            Shoot();
            CShootCountdown = Time.time + ShootCountdown;
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        ShootSound.Play();
        ShootEffect.Play();

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
}
