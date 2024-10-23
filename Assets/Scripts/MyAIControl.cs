using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MyAIControl : MonoBehaviour
{

    GameObject[] goalLocations;
    NavMeshAgent agent;
    Animator anim;
    float speedMult;
    float detectionRadius = 20;
    float fleeRadius = 10;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        goalLocations = GameObject.FindGameObjectsWithTag("Goal");
        int i = Random.Range(0, goalLocations.Length);
        agent.SetDestination(goalLocations[i].transform.position);

        anim.SetFloat("wOffset", Random.Range(0,1));

        ResetAgent();
    }

    void ResetAgent()
    {
        speedMult = Random.Range(0.5f, 2);
        anim.SetFloat("speedMult", speedMult);
        agent.speed = 2 * speedMult;

        anim.SetTrigger("isWalking");

        agent.angularSpeed = 120;

        agent.ResetPath();
    }

    public void DetectNewObstacle(Vector3 position)
    {
        if(Vector3.Distance(position, transform.position) < detectionRadius)
        {
            Vector3 fleeDir = (transform.position - position).normalized;
            Vector3 newGoal = transform.position + fleeDir * fleeRadius;

            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(newGoal, path);

            if(path.status != NavMeshPathStatus.PathInvalid)
            {
                agent.SetDestination(path.corners[path.corners.Length - 1]);
                anim.SetTrigger("isRunning");
                agent.speed = 10;
                agent.angularSpeed = 500;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 1)
        {
            ResetAgent();
            int i = Random.Range(0, goalLocations.Length);
            agent.SetDestination(goalLocations[i].transform.position);
        }
    }
}
