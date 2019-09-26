using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("스피드 조정 변수")]
    [SerializeField]
    private float walkSpeed = 0;
    [SerializeField]
    private float runSpeed;
    private float applySpeed;
    [SerializeField]
    private float crouchSpeed;

    [SerializeField]
    private float jumpForce;

    //상태변수
    private bool isWalk = false;
    private bool isRun = false;
    private bool isGround = true;
    private bool isCrouch = false;

    //움직임 체크 변수
    private Vector3 lastPos;


    //땅 착지 여부
    private CapsuleCollider capsuleCollider;

    //앉았을 때 얼마나 앉을지 결정 변수
    [SerializeField]
    private float crouchPosY;
    private float originPosY; //원래 높이
    private float applyCrouchPosY;


    //카메라 민감도
    [SerializeField]
    private float lookSensitivity = 0; //카메라 민감도

    //카메라 컴포넌트
    [SerializeField]
    private float cameraRotationLimit = 0f; //카메라 y축값 제한
    private float currentCameraRotationX = 0f;

    [SerializeField]
    private Camera theCamera; //카메라
    private Rigidbody myRigid; //플레이어 자체
    private GunController theGunController;
    private CrossHair theCrossHair;


    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        theCamera = FindObjectOfType<Camera>();
        myRigid = GetComponent<Rigidbody>();
        theGunController = FindObjectOfType<GunController>();
        theCrossHair = FindObjectOfType<CrossHair>();


        //초기화
        applySpeed = walkSpeed;
        originPosY = theCamera.transform.localPosition.y; //카메라 자체를 낮춘다
        applyCrouchPosY = originPosY;
    }

    void Update()
    {
        IsGround();
        TryJump();
        TryRun();
        TryCrouch();
        Move();
        MoveCheck();
        CameraRotation();
        characterRotation();
    }

    //앉기 시도
    private void TryCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }

    private void Crouch()
    {
        isCrouch = !isCrouch;// true면 false로 false면  true로
        theCrossHair.CrouchingAnimation(isCrouch);

        if (isCrouch)//true
        {
            applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
        }
        else
        {
            applySpeed = walkSpeed;
            applyCrouchPosY = originPosY;
        }
        StartCoroutine(CrouchCoroutine());
    }

    /// <summary>
    /// 앉는 동작을 부드럽게 하기 위함
    /// </summary>
    IEnumerator CrouchCoroutine()
    {
        float posY = theCamera.transform.localPosition.y; //카메라가 캐릭터의  자식 위치를 기준으로 움직여야 해서 localPosition을씀
        int count = 0;
        while (posY != applyCrouchPosY)
        {
            count++;
            posY = Mathf.Lerp(posY, applyCrouchPosY, 0.3f);
            theCamera.transform.localPosition = new Vector3(0, posY, 0);
            if (count > 15)
                break;
            yield return null;
        }

        theCamera.transform.localPosition = new Vector3(0f, applyCrouchPosY, 0f);
    }

    //땅에 붙어있는지 유무
    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.3f);
        theCrossHair.JumpingAnimation(!isGround);

    }

    void TryJump()//점프시도
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }

    private void Jump()
    {
        //앉은 상태에서 점프시 앉은 상태 해제
        if (isCrouch)
            Crouch();
        myRigid.velocity = transform.up * jumpForce;
    }

    //이동 함수
    private void Move()
    {
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * moveDirX;
        Vector3 moveVertical = transform.forward * moveDirZ;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * applySpeed;

        myRigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    private void MoveCheck()
    {
        if (!isRun && !isCrouch && isGround)
        {
            if (Vector3.Distance(lastPos, transform.position) >= 0.01f)
            {
                isWalk = true;
            }
            else
            {
                isWalk = false;

            }
            theCrossHair.WalkingAnimation(isWalk);
            lastPos = transform.position;
        }

    }

    //뛰기 시도
    void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
        }
    }

    private void Running()
    {
        if (isCrouch)
            Crouch();

        theGunController.CancelFineSight();
       
        isRun = true;
        theCrossHair.RunningAnimation(isRun);

        applySpeed = runSpeed;
    }

    private void RunningCancel()
    {
        isRun = false;
        theCrossHair.RunningAnimation(isRun);
        applySpeed = walkSpeed;
    }

    //캐릭터 회전
    private void characterRotation()
    {
        //좌우 캐릭터 회전
        float yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 characterRotationY = new Vector3(0f, yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(characterRotationY));
    }

    private void CameraRotation()
    {
        //상하 카메라 회전
        float xRotation = Input.GetAxisRaw("Mouse Y");//위아래로 고개를 돌림
        float camearaRotationX = xRotation * lookSensitivity;
        currentCameraRotationX -= camearaRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit); //카메라 각도 제한

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }


}
