using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    Zombi zombies;
    NavMeshAgent agent;
    public AttackBehaviour attackStyle;
    Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    //Gezinme
    Vector3 walkPoint=Vector3.zero;
    bool walkPointSet;
    public float walkPointRange;
    //Attacking
    float timeBetweenAttacks=2f;
    bool alreadyAttacked=false;
    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private void Awake()
    {
        attackStyle = GetComponent<AttackBehaviour>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange) AttackPlayer();
    }
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet) agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }

    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float t_randomZ = Random.Range(-walkPointRange, walkPointRange);
        float t_randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + t_randomX, transform.position.y, transform.position.z + t_randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }

    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        //Make sure enemy doesnt move
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attack code here
            attackStyle.Attack();
            alreadyAttacked = true;
            
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
           
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    
}
 

