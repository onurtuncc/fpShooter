using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieGenerator : MonoBehaviour
{
    public Zombi[] zombies;
    Vector3 position=Vector3.zero;
    private void Start()
    {
        GenerateZombies();
    }
    void GenerateZombies()
    {
        foreach (Zombi zombi in zombies)
        {
            Instantiate(zombi.prefab, position, Quaternion.identity);
        }
    }
    

}
