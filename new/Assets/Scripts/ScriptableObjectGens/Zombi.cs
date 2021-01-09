using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Zombie", menuName = "Zombie")]
public class Zombi : ScriptableObject
{
    public int damage;
    public int health;
    public float speed;
    public GameObject prefab;
   
}
