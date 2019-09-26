using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxeController : CloseWeaponController
{
    public static bool isActivate = true;

    private void Start()
    {
        WeaponManager.currentWeapon = currentCloseWeapon.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentCloseWeapon.anim;

    }



    void Update()
    {
        if (isActivate)
        {
            TryAttack();
        }
    }



    protected override IEnumerator HitCoroutine()
    {
        while (isSwing)//팔이 돌아가는동안 접촉한게 있는지 없는지 판단
        {
            if (CheckObject())
            {
               if(hitInfo.transform.tag == "Rock")
                {
                    hitInfo.transform.GetComponent<Rock>().Mining();
                }
               else if(hitInfo.transform.tag =="Twig")
                {
                    hitInfo.transform.GetComponent<Twig>().Damage(this.transform);
                }
                isSwing = false;
                //충돌함
                Debug.Log(hitInfo.transform.name);

            }
            yield return null;//1초대기
        }
    }

    public override void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        base.CloseWeaponChange(_closeWeapon);
        isActivate = true;

    }
}
