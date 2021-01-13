using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    #region variables
    public int health = 1000;
    public float sightRange, attackRange;
    public NavMeshAgent agent;
    Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    //Gezinme
    Vector3 walkPoint=Vector3.zero;
    bool walkPointSet;
    public float walkPointRange;
    //States
   
    public bool playerInSightRange, playerInAttackRange=false;
    #endregion
    #region monobehavioue callbacks
    private void Awake()
    {
        
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
        
    }
    #endregion
    #region private methods
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
    public void TakeDamage(int takenDamage)
    {
        health -= takenDamage;
        if (health <= 0)
        {
            //Play die animation
            Destroy(gameObject, 0.5f);
        }
    }
    #endregion


}


