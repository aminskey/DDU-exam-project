using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class EnemyScript : MonoBehaviour
{
    [SerializeField] float detectionRange = 10f;
    [SerializeField] float followRange = 4f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] Transform head;
    [SerializeField] LayerMask mask;
    [SerializeField] float attackCooldown = 1f;
    [SerializeField] Transform player;
    [SerializeField] bool dontDie=false;
    [SerializeField] bool nextScene = false;
    [SerializeField] bool knockback = false;
    [SerializeField] bool canSprint = false;

    float cooldown, speed;
    NavMeshAgent agent;
    Animator anim;
    PlayerVariables p;
    PlayerVariables p1;
    Rigidbody rb, rb2;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        cooldown = attackCooldown;
        p = GetComponent<PlayerVariables>();
        rb = GetComponent<Rigidbody>();
        rb2 = player.gameObject.GetComponent<Rigidbody>();
        p1 = player.gameObject.GetComponent<PlayerVariables>();

        speed = agent.speed;
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

        if (p.health <= 0f)
        {
            if (!anim.GetBool("IsDying"))
            {
                rb.constraints = rb.constraints | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
                anim.SetBool("IsDying", true);
                agent.ResetPath();
                if(!dontDie) Invoke("Die", 7f);
                if (nextScene) Invoke("NextScene", 4f);
            }
        }
        else
        {
            if (p1.health > 0f)
                DetectPlayer();
            else {
                Debug.Log(p1.health);
                anim.SetBool("IsAttacking", false);
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsDying", false);
            }
            cooldown -= Time.deltaTime;
        }

        if (!anim.GetBool("IsAttacking"))
        {
            if (agent.velocity.magnitude > 0f)
            {
                if ((transform.position - player.position).magnitude > 40f || (transform.position - player.position).magnitude < 3f)
                {
                    anim.SetBool("IsRunning", false);
                    anim.SetBool("IsWalking", true);
                    agent.speed = speed;
                   
                } 
                else
                {
                    anim.SetBool("IsWalking", false);
                    anim.SetBool("IsRunning", true);
                    agent.speed = 2f * speed;

                }
            }
            else
            {
                anim.SetBool("IsWalking", false);
                anim.SetBool("IsRunning", false);
            }
        }


        Debug.Log(agent.velocity.magnitude);


    }
    void DetectPlayer()
    {
        Vector3 vec = transform.position - player.position;
        float dist = vec.magnitude;
        if (dist < detectionRange)
        {
            if (dist < followRange)
            {
                // head.LookAt(target -> Vector3)
                head.LookAt(new Vector3(vec.x, transform.position.y, vec.z));
                agent.SetDestination(player.position);
                if (dist <= attackRange)
                {
                    agent.velocity = Vector3.zero;
                    if (!anim.GetBool("IsAttacking") && cooldown <= 0f)
                    {
                        anim.SetBool("IsAttacking", true);
                        explosionForce();
                        
                        cooldown = attackCooldown;
                    }
                }
            }
        }
    }

    void explosionForce()
    {
        rb2.AddExplosionForce(50f, player.position, 10f);
    }

    void DetectClosestFighter()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, detectionRange, mask);
        Collider closestFighter = null;

        float closestDist = Mathf.Infinity;
        foreach (Collider coll in colls)
        {
            if (coll.transform != transform)
            {
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

    public void Die()
    {
        Destroy(transform.gameObject);
    }

    void NextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
