using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    #region variables
    public Gun[] loadout;
    public Transform weaponParent;
    GameObject currentWeapon;
    EnemyAI enemyai;
    float currentCooldown=0;
    int currentIndex;
    public GameObject bulletHolePrefab;
    public LayerMask canBeShot;
    public Animator gunAnim;
    public Animator topAnim;
    int sarjor = 0;
    int fullsarjor = 0;
    public Text sarjorText;
    bool isReloading = false;
    public Text fullSarjorText;
    #endregion
    #region monobehaviour callbacks
    void Start()
    {
        
        currentWeapon = null;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentWeapon != null)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (loadout[currentIndex].sarjorKapasite > sarjor && fullsarjor>0)
                {
                    Reload();
                }
            }
            Aim(Input.GetMouseButton(1));
            if (Input.GetMouseButton(0) && currentCooldown<=0 && sarjor>0 && !isReloading)
            {
                Shoot();
                
            }
            

            //cooldown
            if (currentCooldown > 0) currentCooldown -= Time.deltaTime;

        }

    }
    #endregion
    #region private methods
    private void Reload()
    {

        topAnim.SetTrigger("reload");
        gunAnim.SetTrigger("reload");
        isReloading = true;
        StartCoroutine(SarjorFulle());

    }

   

    IEnumerator SarjorFulle()
    {
        int t_sarjkapasite = loadout[currentIndex].sarjorKapasite;
        
        yield return new WaitForSeconds(4f);
        if (t_sarjkapasite -sarjor> fullsarjor)
        {
            
            sarjor += fullsarjor;
            fullsarjor=0;
        }
        else
        {
            fullsarjor -= t_sarjkapasite - sarjor;
            sarjor = t_sarjkapasite;
        }
        
        
        sarjorText.text = sarjor.ToString();
        fullSarjorText.text = fullsarjor.ToString();
        isReloading = false;
    }
    public void Equip(int weaponIndex)
    {
        if (currentWeapon != loadout[weaponIndex])
        {
            Destroy(currentWeapon);
            currentIndex = weaponIndex;
            GameObject t_newEquipment = Instantiate(loadout[weaponIndex].prefab, weaponParent.position, weaponParent.rotation,weaponParent) as GameObject;
            t_newEquipment.transform.localPosition = Vector3.zero;
            t_newEquipment.transform.localEulerAngles = Vector3.zero;
            currentWeapon = t_newEquipment;
            t_newEquipment.transform.SetParent(t_newEquipment.transform);
            gunAnim = t_newEquipment.GetComponentInChildren<Animator>();
            sarjor = loadout[currentIndex].sarjorKapasite;
            fullsarjor = sarjor * 3;
            sarjorText.text = sarjor.ToString();
            fullSarjorText.text = fullsarjor.ToString();
           
        }
        
    }
    void Aim(bool is_aiming)
    {
        Transform t_anchor = currentWeapon.transform.Find("Anchor");
        Transform t_ads = currentWeapon.transform.Find("States/ADS");
        Transform t_hip = currentWeapon.transform.Find("States/Hip");
        if (is_aiming)
        {
            //Aim-ADS
            t_anchor.position = Vector3.Lerp(t_anchor.position, t_ads.position,Time.deltaTime*loadout[currentIndex].aimspeed);
        }
        else
        {
            //hip
            t_anchor.position = Vector3.Lerp(t_anchor.position, t_hip.position, Time.deltaTime * loadout[currentIndex].aimspeed);
        }
    }
    void Shoot()
    {
        
        Transform t_spawn = transform.Find("Top/Normal Camera");
        //Silahın sekmesi
        Vector3 t_sekme = t_spawn.position +t_spawn.forward*1000f;
        t_sekme += UnityEngine.Random.Range(-loadout[currentIndex].bloom, loadout[currentIndex].bloom) * t_spawn.up;
        t_sekme += UnityEngine.Random.Range(-loadout[currentIndex].bloom, loadout[currentIndex].bloom) * t_spawn.right;
        t_sekme -= t_spawn.position;
        t_sekme.Normalize();
        gunAnim.SetTrigger("firing");
        topAnim.SetTrigger("firing");
        //raycast
        RaycastHit t_hit = new RaycastHit();
        if(Physics.Raycast( t_spawn.position,t_sekme,out t_hit, 1000f, canBeShot))
        {
            Transform t_parent = null;
            //Checking if we shoot enemy
            if (t_hit.collider.gameObject.layer == 11)
            {
                t_parent = t_hit.collider.gameObject.transform;
                enemyai = t_hit.collider.gameObject.GetComponent<EnemyAI>();
                enemyai.TakeDamage(loadout[currentIndex].damage);
                
                
            }
            GameObject t_newHole = Instantiate(bulletHolePrefab, t_hit.point + t_hit.normal * 0.001f, Quaternion.identity,t_parent) as GameObject;
            t_newHole.transform.LookAt(t_hit.point + t_hit.normal);
            Destroy(t_newHole, 5f);
            

        }
        
        //cooldown
        currentCooldown = loadout[currentIndex].firerate;
        sarjor -= 1;
        sarjorText.text = sarjor.ToString();
        if (sarjor == 0 && fullsarjor>0)
        {
            Reload();
            
        }
        


    }
    #endregion

}
