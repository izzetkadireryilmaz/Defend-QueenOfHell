using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    NavMeshAgent Agent;
    GameObject Target;
    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
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


}
