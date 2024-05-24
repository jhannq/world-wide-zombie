using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAISpider : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    //public float timeBetweenAttacks;
    //bool alreadyAttacked;
    //public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    [SerializeField]
    Transform shootPoint;

    [SerializeField]
    GameObject poison;

    float fireRate = 3f;


    //public Animator animator;

    private void Awake()
    {
        player = GameObject.Find("Character").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        //if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();

            if (agent.name == "Normal Zombie")
            {

            }
            //animator.SetBool("NZ_Walk", true);
        }

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        transform.LookAt(player);

        if (gameObject.name == "Spitter Zombie")
        {
            fireRate -= Time.deltaTime;

            if (fireRate <= 0)
            {
                fireRate = 3f;
                Shoot();
            }

            //Shoot();
        }
    }

    void Shoot()
    {
        Vector3 positionProjectile = new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z);
        Instantiate(poison, positionProjectile, transform.rotation);
    }
}
