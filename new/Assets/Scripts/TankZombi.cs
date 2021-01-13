using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankZombi : MonoBehaviour
{
    // Start is called before the first frame update
    #region Variables
    int damage = 40;
    Transform player;
    bool alreadyAttacked = false;
    EnemyAI ai;
    int timeBetweenAttacks = 4;
    HealthManager healthManager;
    #endregion
    #region Monobehaviour callbacks
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
    #region Private methods
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
