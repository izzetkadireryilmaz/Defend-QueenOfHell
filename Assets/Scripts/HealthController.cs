using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    float health;
    public Image HealthBar;
    public GameObject parents, scoreText;
    public Canvas deadCanvas, Gamecanvas;
    public Camera MyCam;

    void Start()
    {
        health = 100;
        HealthBar.fillAmount = health;
        deadCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeHealth();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        HealthBar.fillAmount = health / 100;

        if (health <= 0)
        {
            Time.timeScale = 0;
            parents.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
            parents.GetComponent<Animator>().Play("PlayerDead");
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSecondsRealtime(1f);
        deadCanvas.gameObject.SetActive(true);
    }


    void TakeHealth()
    {
        RaycastHit hit;

        if (Physics.Raycast(MyCam.transform.position, MyCam.transform.forward, out hit, 4))
        {
            if (hit.transform.gameObject.CompareTag("AidKit"))
            {
                Destroy(hit.transform.gameObject);
                health += 30;
                HealthBar.fillAmount = health / 100;
            }
        }
    }

}
