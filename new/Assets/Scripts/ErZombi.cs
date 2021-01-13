using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErZombi : MonoBehaviour
{
    #region variables
    int damage = 25;
    Transform player;
    bool alreadyAttacked = false;
    EnemyAI ai;
    int timeBetweenAttacks = 2;
    HealthManager healthManager;
    #endregion
    #region monobehaviour callbacks
    private void Update()
    {
        if (ai.playerInAttackRange) AttackPlayer();
    }
    private void Awake()
    {
        ai = GetComponent<EnemyAI>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        healthManager = player.gameObject.GetComponent<HealthManager>();
    }
    #endregion
    #region private methods
    public void AttackPlayer()
    {
        //Make sure enemy doesnt move
        ai.agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attack code here
            //Do enemy animation and behaviour
            DoDamage();
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    void DoDamage()
    {
        healthManager.TakeDamage(damage);
    }
    #endregion


}
