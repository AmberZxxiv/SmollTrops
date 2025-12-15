using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Control : MonoBehaviour
{// script en cada enemigo
 //pillo SINGLEs del PC y MC
   public Player_Control _PC;
   public Menus_Control _MC;

    #region /// MOVIMIENTO Y TRACKING ///
    private NavMeshAgent agent; //IA propia
    public Transform target; //objetivo al que ir
    public float agroDistance; //agro 
    private float targetDistance; //comprobacion
    public float wanderRadius; //zona de patrulla
    #endregion

    #region /// ATTACK STATS ///
    public Transform attackOrigin;
    public float attackRange;
    public float attackDamage;
    public float attackForce;
    bool _canAttack = true;
    #endregion

    #region /// COOLDOWN CONTROL ///
    public float wanderDelay; //cada cuanto spatrulla
    public float wanderTimer; //contador interno
    public float attackCooldown; //cada cuanto ataca
    #endregion

    public float health; // vida de cada enemigo
    public MeshRenderer meshRenderer; //render del material
    private Color originalColor;

    void Start()
    {
        //pillo SINGLEs del PC y MC
        _PC = Player_Control.instance;
        _MC = Menus_Control.instance;
        target = _PC.transform; // le doy el transform del PC como target
        agent = GetComponent<NavMeshAgent>(); //pillo IA propia
        // desde donde se van a generar los ataques
        if (attackOrigin == null)
        { attackOrigin = this.transform;}
        // le asignamos un material individual a cada enemigo
        meshRenderer.material = new Material(meshRenderer.material);
        originalColor = meshRenderer.material.color;
    }

    void Update()
    {
        //compruebo distancia con player
        targetDistance = Vector3.Distance(agent.transform.position, target.position);
        // si esta a rango de ataque, ataco
        if (targetDistance <= attackRange && _canAttack)
        {
            agent.SetDestination(transform.position);
            DoATTACK();
        }
        // cuando pilla agro, va hacia el player
        if (targetDistance <= agroDistance)
        {
            agent.SetDestination(target.position);
        }
        // si no, patrulla
        else Wander();
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
        if (!found) return origin;
        else return navHit.position;
    }

    void DoATTACK()
    {
        _canAttack = false;
        Vector3 dir = target.position - attackOrigin.position;
        dir.y = 0; dir.Normalize();

        Vector3 halfExtents = new Vector3(1f, 1f, attackRange);
        Vector3 center = attackOrigin.position + dir * attackRange;

        Collider[] hits = Physics.OverlapBox(center,halfExtents,Quaternion.LookRotation(dir));

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                _PC.health -= 2;
                _PC.StartCoroutine(_PC.FlashDamage());
                Vector3 hitDir = (_PC.transform.position - transform.position).normalized;
                _PC.ApplyKnockback(hitDir, attackForce);
                StartCoroutine(AttackCooldown());
            }
        }
    }
    IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        _canAttack = true;
    }

    public void TakeDamage(float damage)//llamo desde las weapons para hitear enemys
    {
        StartCoroutine(FlashDamage());
        health -= damage;
        if (health <= 0)
        {
            if (CompareTag("boss"))
            {
                _MC.ShowVictory();
            }
            Destroy(gameObject);
        }
    }
    public IEnumerator FlashDamage()//efecto de daño al hitear enemys
    {
        meshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(1f);
        meshRenderer.material.color = originalColor;
    }
}

