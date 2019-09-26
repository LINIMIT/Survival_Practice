using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    //기존위치
    private Vector3 originPos;
    //현재 위치
    private Vector3 currentPos;

    [SerializeField]
    private Vector3 limitPos;

    [SerializeField]
    private Vector3 fineSightLimitPos;

    //부드러운 정도
    [SerializeField]
    private Vector3 smoothSway;

    [SerializeField]
    private GunController theGunController;



    void Start()
    {
        originPos = this.transform.localPosition;

    }

    void Update()
    {
        TrySway();
    }

    private void TrySway()
    {
        if (Input.GetAxisRaw("Mouse X") != 0 || Input.GetAxisRaw("Mouse Y") != 0)
        {
            Swaying();
        }
        else
        {
            BackToOriginPos();
        }
    }

    private void Swaying()
    {
        float _moveX = Input.GetAxisRaw("Mouse X");
        float _moveY = Input.GetAxisRaw("Mouse Y");

        //정조준 상태가 아닐시의 흔들림
        if (!theGunController.isFineSightMode)
            currentPos.Set(Mathf.Clamp(Mathf.Lerp(currentPos.x, -_moveX, smoothSway.x), -limitPos.x, limitPos.x),
                                            Mathf.Clamp(Mathf.Lerp(currentPos.y, -_moveY, smoothSway.x), -limitPos.y, limitPos.y),
                                            originPos.z);
        else //정조준 상태일시의 흔들림
        {
            currentPos.Set(Mathf.Clamp(Mathf.Lerp(currentPos.x, -_moveX, smoothSway.y), -fineSightLimitPos.x, fineSightLimitPos.x),
                                         Mathf.Clamp(Mathf.Lerp(currentPos.y, -_moveY, smoothSway.y), -fineSightLimitPos.y, fineSightLimitPos.y),
                                         originPos.z);
        }
        transform.localPosition = currentPos;


    }

    private void BackToOriginPos()
    {
        currentPos = Vector3.Lerp(currentPos, originPos , smoothSway.x);
        transform.localPosition = currentPos;
    }



}
