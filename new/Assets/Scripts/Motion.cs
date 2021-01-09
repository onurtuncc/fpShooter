using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    float speed = 400f;
    Rigidbody rig;
    float jumpForce = 150f;
    float sprintModifier = 2f;
    public Weapon weaponScript;
    public Camera normalcam;
    float baseFOV;
    float sprintFOVModifier = 1.25f;
    float tfovTransitionTime = 8f;
    public Transform groundDetector;
    public LayerMask ground;
    public Animator topAnim;
    public Animator legAnim;
    public GameObject gun;
    public GameObject gunPos;
    private Animator gunAnim;
    float distToGround;

    // Update is called once per frame
    private void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;
        
        baseFOV = normalcam.fieldOfView;

        Camera.main.enabled = false;
        rig = GetComponent<Rigidbody>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("lootable"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                weaponScript.Equip(0);
                gunAnim = weaponScript.gunAnim;
                topAnim.SetBool("keleş", true);
                Destroy(other.attachedRigidbody.gameObject);

            }

        }
        
    }
    void Update()
    {
        //Yönler
        float t_hmove = Input.GetAxisRaw("Horizontal");
        float t_vmove = Input.GetAxisRaw("Vertical");

        //Kontroller
        bool sprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool jump = Input.GetKey(KeyCode.Space);

        //Durumlar
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down,distToGround+0.1f, ground);
        bool isJumping = jump && isGrounded;
        bool isSprinting = sprint && t_vmove > 0 && !isJumping && isGrounded;
        topAnim.SetBool("sprint", isSprinting);
        



        //Zıplama
        if (isJumping)
        {
            rig.AddForce(Vector3.up * jumpForce);
        }

        //Hareket
        Vector3 t_direction = new Vector3(t_hmove, 0, t_vmove);
        t_direction.Normalize();

        float t_adjustedSpeed = speed;
        if (isSprinting)
        {
            t_adjustedSpeed *= sprintModifier;
            normalcam.fieldOfView = Mathf.Lerp(normalcam.fieldOfView, baseFOV * sprintFOVModifier, Time.deltaTime * tfovTransitionTime);

        }
        else
        {
            normalcam.fieldOfView = Mathf.Lerp(normalcam.fieldOfView, baseFOV, Time.deltaTime * tfovTransitionTime);
        }

        Vector3 t_targetVelocity = transform.TransformDirection(t_direction) * t_adjustedSpeed * Time.deltaTime;
        t_targetVelocity.y = rig.velocity.y;
        rig.velocity = t_targetVelocity;
       

        legAnim.SetFloat("speed", Math.Abs(t_vmove));
        topAnim.SetFloat("speed", Math.Abs(t_vmove));

        if (topAnim.GetBool("keleş"))
        {
            gunAnim.SetFloat("speed", Math.Abs(t_vmove));
            gunAnim.SetBool("sprint", isSprinting);


        }
        
    }
   

}