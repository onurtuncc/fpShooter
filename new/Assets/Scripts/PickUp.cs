using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Weapon weapon = other.attachedRigidbody.gameObject.GetComponent<Weapon>();
            if (Input.GetKeyDown(KeyCode.F))
            {
                weapon.Equip(0);
                Destroy(gameObject);
            }    
            
        }
           
        
    }
}
