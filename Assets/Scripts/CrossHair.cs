﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private float gunAccuracy; //크로스헤어 상태에 따른 총의 정확도

    [SerializeField]
    private GameObject go_CrossHairHUD;
    [SerializeField]
    private GunController theGunController;


    public void WalkingAnimation(bool _flag)
    {
        WeaponManager.currentWeaponAnim.SetBool("Walk", _flag);
        animator.SetBool("Walking", _flag);
    }

    public void RunningAnimation(bool _flag)
    {
        WeaponManager.currentWeaponAnim.SetBool("Run", _flag);
        animator.SetBool("Running", _flag);
    }

    public void JumpingAnimation(bool _flag)
    {
        animator.SetBool("Running", _flag);
    }

    public void CrouchingAnimation(bool _flag)
    {
        animator.SetBool("Crouching", _flag);
    }
    public void FineSightAnimation(bool _flag)
    {
        animator.SetBool("FineSight", _flag);
    }


    public void FireAnimation()
    {
        if(animator.GetBool("Walking"))
        {
            animator.SetTrigger("Walk_Fire");
        }
        else if(animator.GetBool("Crouching"))
        {
            animator.SetTrigger("Crouch_Fire");
        }
        else
        {
            animator.SetTrigger("Idle_Fire");
        }
    }

    public float GetAccuarcy()
    {
        if (animator.GetBool("Walking"))
        {        
            gunAccuracy = 0.06f;
        }
        else if (animator.GetBool("Crouching"))
        {       
            gunAccuracy = 0.015f;
        }
        else if(theGunController.GetFineSightMode())
        {          
            gunAccuracy = 0.001f;
        }
        else
        {
            gunAccuracy = 0.035f;
        }
        return gunAccuracy;
    }
}
