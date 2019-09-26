using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : CloseWeaponController
{
    public static bool isActivate = false;



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
                isSwing = false;
                //충돌함
                Debug.Log(hitInfo.transform.name);

            }
            yield return null;//1초대기
        }
    }

    public override void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        base.CloseWeaponChange(_closeWeapon);//부모를 가져옴
        isActivate = true;

    }
}
