using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour
{
    int enemyHealth = 1000;
    HealthManager health;
    int damage = 50;
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        health = player.GetComponent<HealthManager>();
    }
    public void Attack()
    {
        //AttackBehaviour
        DoDamage(damage);
    }
    public void DoDamage(int dmg)
    {
        //play Attack animation
        health.TakeDamage(dmg);
    }
    public void TakeDamage(int dmg)
    {
        //play Take Damage animation
        enemyHealth -= dmg;
        if (enemyHealth <= 0)
        {
            Destroy(gameObject, 1f);
        }
    }
}
