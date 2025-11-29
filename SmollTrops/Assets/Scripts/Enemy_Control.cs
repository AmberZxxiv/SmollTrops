using UnityEngine;
using UnityEngine.AI;

public class Enemy_Control : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target;
    public float attackDistance;
    private float targetDistance;
    public float wanderRadius;
    public float wanderDelay;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        target = player.transform;
    }

    void Update()
    {
        targetDistance = Vector3.Distance(agent.transform.position, target.position);
        if (targetDistance <= attackDistance)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            Wander();
        }
    }

    void Wander()
    {
        float wanderTimer =- Time.deltaTime;
        if (wanderTimer <= 0f)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            wanderTimer = wanderDelay;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }
}

