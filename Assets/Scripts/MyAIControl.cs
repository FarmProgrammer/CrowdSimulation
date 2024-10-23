using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MyAIControl : MonoBehaviour
{

    GameObject[] goalLocations;
    NavMeshAgent agent;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        goalLocations = GameObject.FindGameObjectsWithTag("Goal");
        int i = Random.Range(0, goalLocations.Length);
        agent.SetDestination(goalLocations[i].transform.position);

        anim.SetTrigger("isWalking");
        anim.SetFloat("wOffset", Random.Range(0,1));
        float sm = Random.Range(0.5f, 2);
        anim.SetFloat("speedMult", sm);
        agent.speed *= sm;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 1)
        {
            int i = Random.Range(0, goalLocations.Length);
            agent.SetDestination(goalLocations[i].transform.position);
        }
    }
}
