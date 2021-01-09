using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Text healthText;
    int health = 100;
    bool alive = true;
    private void Update()
    {
        
        
    }
    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
            healthText.text = "0";
        }
        else
        {
            healthText.text = health.ToString();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody.gameObject.tag.Equals("medkit"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SetHealth(200);
                Destroy(other.attachedRigidbody.gameObject);
            }
            
        }
    }
    void SetHealth(int new_health)
    {
        health = new_health;
        healthText.text = health.ToString();
    }
    void Die()
    {
        alive = false;
    }

}
