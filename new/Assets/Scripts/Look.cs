using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    #region variables
    public Transform player;
    public Transform cams;
    public Transform weapon;
    static bool curserLocked=true;
    float xSens=100;
    float ySens=100;
    float maxAngle=60;
    Quaternion camCenter;

    #endregion
    #region Monobehaviour Callbacks
    void Start()
    {
        camCenter = cams.localRotation;
    }

    
    void Update()
    {
        SetY();
        SetX();
        UpdateCursorLock();
    }
    #endregion

    #region Private Methods
    void SetY()
    {
        float t_input = Input.GetAxis("Mouse Y")*ySens*Time.deltaTime;
        Quaternion t_adjustment = Quaternion.AngleAxis(t_input,-Vector3.right);
        Quaternion t_delta = cams.localRotation * t_adjustment;
        if (Quaternion.Angle(camCenter, t_delta) < maxAngle)
        {
            cams.localRotation = t_delta;
        }
        weapon.rotation = cams.rotation;

    }
    void SetX()
    {
        float t_input = Input.GetAxis("Mouse X") * xSens * Time.deltaTime;
        Quaternion t_adjustment = Quaternion.AngleAxis(t_input, Vector3.up);
        Quaternion t_delta = player.localRotation * t_adjustment;
        player.localRotation = t_delta;
    }
    void UpdateCursorLock()
    {
        if (curserLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                curserLocked = false;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                curserLocked = true;
            }
        }
    }
    #endregion
}
