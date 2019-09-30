using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//추상 클래스
public abstract class CloseWeaponController : MonoBehaviour
{

    //현재 장착된 Hand
    [SerializeField]
    protected CloseWeapon currentCloseWeapon;

    //공격중
    protected bool isAttack = false;
    protected bool isSwing = false; //팔 휘두루는지

    protected RaycastHit hitInfo;



    protected void TryAttack()
    {
        if (Input.GetButton("Fire1"))
        {
            if (!isAttack)
            {
                if (CheckObject())
                {
                    if (currentCloseWeapon.isAxe && hitInfo.transform.tag == "Tree")
                    {
                        //코루틴 실행
                        StartCoroutine(AttackCoroutine("Chop", currentCloseWeapon.workDelayA, currentCloseWeapon.workDelayB, currentCloseWeapon.workDelay));
                        return;
                    }

                }
                StartCoroutine(AttackCoroutine("Attack", currentCloseWeapon.attackDelayA, currentCloseWeapon.attackDelayB, currentCloseWeapon.attackDelay));

            }
        }
    }

    protected IEnumerator AttackCoroutine(string swingType, float _delayA, float _delayB, float _delayC)
    {
        isAttack = true; //중복실행 막음
        currentCloseWeapon.anim.SetTrigger(swingType);

        yield return new WaitForSeconds(_delayA);
        isSwing = true;

        //공격 활성화 시점
        StartCoroutine(HitCoroutine());//적중여부 판단


        yield return new WaitForSeconds(_delayB);
        isSwing = false;

        yield return new WaitForSeconds(_delayC - _delayA - _delayB);
        isAttack = false;
    }

    //자식클래스가 완성시킴
    protected abstract IEnumerator HitCoroutine();


    protected bool CheckObject()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, currentCloseWeapon.range))
        {
            return true;
        }
        return false;
    }



    //추가 편집이 가능한 함수
    public virtual void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        if (WeaponManager.currentWeapon != null)//들고있는 경우에
        {
            WeaponManager.currentWeapon.gameObject.SetActive(false);
        }
        currentCloseWeapon = _closeWeapon;
        WeaponManager.currentWeapon = currentCloseWeapon.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentCloseWeapon.anim;


        currentCloseWeapon.transform.localPosition = Vector3.zero;
        currentCloseWeapon.gameObject.SetActive(true);

    }
}
