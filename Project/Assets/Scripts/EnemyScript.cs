using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] float health=1f;
    [SerializeField] float detectionRange = 10f;
    [SerializeField] float followRange = 4f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] Transform head;
    [SerializeField] LayerMask mask;
    [SerializeField] float attackCooldown = 1f;
    [SerializeField] Transform player;


    float cooldown;
    NavMeshAgent agent;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        cooldown = attackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (cooldown <= 0)
        {
            DetectPlayer();
            cooldown = 2f;
        }
        cooldown -= Time.deltaTime;
        */
        DetectPlayer();
        cooldown -= Time.deltaTime;
        
        
    }
    void DetectPlayer()
    {
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist < detectionRange)
        {
            
            if(dist < followRange)
            {
                head.LookAt(player);
                agent.SetDestination(player.position);
                if (dist <= attackRange && !anim.GetBool("IsAttacking") && cooldown <= 0f) {
                    anim.SetBool("IsAttacking", true);
                    cooldown = attackCooldown;
                }
            } else
            {
                agent.ResetPath();
            }
        }
    }

    void DetectClosestFighter()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, detectionRange, mask);
        Collider closestFighter = null;

        float closestDist = Mathf.Infinity;
        foreach (Collider coll in colls)
        {
            if (coll.transform != transform) {
                float dist = Vector3.Distance(transform.position, coll.transform.position);
                if (dist <= detectionRange && dist <= followRange)
                {
                    closestFighter = coll;
                }
            }
        }

        if (closestFighter != null)
        {
            head.LookAt(closestFighter.transform);
            agent.SetDestination(closestFighter.transform.position);
        }
    }

    
}
