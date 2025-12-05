using UnityEngine;
using UnityEngine.AI;

public class Enemy_Control : MonoBehaviour
{
    public Player_Control _PC; //singleton del player
    private NavMeshAgent agent; //componente propio
    public Transform target; //objetivo al que ir
    public float attackDistance; //agro 
    private float targetDistance; //comprobacion
    public float wanderRadius; //zona de patrulla
    public float wanderDelay; //cada cuanto se mueve
    public float wanderTimer; //contador interno

    public float health;

    void Start()
    {
        // pillo el singleton del Player. PUEDO PILLAR EL TAG DE AQUI?
        _PC = Player_Control.instance;
        agent = GetComponent<NavMeshAgent>(); //pillo el componente propio
        //busco el tag del player en escena y se lo doy al objetivo
        GameObject player = GameObject.FindGameObjectWithTag("Player"); 
        target = player.transform;
    }

    void Update()
    {
        //compruebo la distancia con el player, pillo el agro o sigo patrullando
        // aquí puedo poner un ternario de estos? con el target?
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
        //empiezo el conteo para cambiar de posicion
        wanderTimer -=Time.deltaTime;
        if (wanderTimer <= 0f)
        { //cuando ha pasado el tiempo, le doy una posición nueva y reinicio el contador
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            wanderTimer = wanderDelay;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    { //aquí genero un Vector3 dentro del radio del enemigo
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        bool found = NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        if (!found)
        {
            return origin;
        }
        return navHit.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           _PC.health -= 2;
        }
    }
}

