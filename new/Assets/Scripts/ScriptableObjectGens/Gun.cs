using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Gun",menuName="Gun")]
public class Gun : ScriptableObject
{
    public string name;
    public float firerate;
    public float bloom;
    public int damage;
    public float aimspeed;
    public GameObject prefab;
    public int sarjorKapasite;
    
    
}
