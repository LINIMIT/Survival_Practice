using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : CloseWeaponController
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

                if(hitInfo.transform.tag == "Grass")
                {
                    hitInfo.transform.GetComponent<Grass>().Damage();
                }
                else if(hitInfo.transform.tag == "Tree")
                {
                    hitInfo.transform.GetComponent<TreeComponent>().Chop(hitInfo.point, transform.eulerAngles.y);
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
