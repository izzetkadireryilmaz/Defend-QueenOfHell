using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    NavMeshAgent Agent;
    GameObject Target;
    public float Health, damage;
    Animator animator;
    bool attacking;
    public GameObject GameManager;

    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        GameManager = GameObject.Find("GameManager");
    }
    public void SetaTarget(GameObject Player)
    {
        Target = Player;
    }
    // Update is called once per frame
    void Update()
    {
        Agent.SetDestination(Target.transform.position);
    }

    public void Damage(float damage)
    {
        Health -= damage;
        Debug.Log(damage);
        if (Health <= 0)
        {
            Dead();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!attacking)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                attacking = true;
                animator.Play("Attack");
                other.transform.gameObject.GetComponent<HealthController>().TakeDamage(damage);
            }
        }

    }
    void canIAttacking()
    {
        attacking = false;
    }
    void Dead()
    {
        GameManager.GetComponent<ScoreScript>().Scoree(1);
        animator.Play("Death");
        Destroy(gameObject);
    }

}
