using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move Setting")]
    [SerializeField] private LayerMask groundLayerMask; // 점프 가능한 레이어
    [SerializeField] private LayerMask wallLayerMask; // 벽 타기 가능한 레이어
    [SerializeField] private int jumpStamina;
    [SerializeField] private int climbStamina;
    public int curJumpIndex = 0;
    public int maxJumpIndex = 1;

    [Header("Look Setting")]
    [SerializeField] private Transform cameraContainer;
    [SerializeField] private float minXLook;  // 최소 시야각
    [SerializeField] private float maxXLook;  // 최대 시야각
    [SerializeField] private float camCurXRot;
    [SerializeField] private float lookSensitivity; // 카메라 민감도
    [SerializeField] private Camera _camera;
    [SerializeField] private float cameraDistance = 5.5f; // 카메라 거리

    private PlayerStamina playerStamina;
    private StatHandler statHandler;
    private InputHandler inputHandler;
    private Rigidbody rb;

    public void Init(PlayerStamina playerStamina, StatHandler statHandler, InputHandler inputHandler)
    {
        rb = GetComponent<Rigidbody>();
        this.playerStamina = playerStamina;
        this.statHandler = statHandler;
        this.inputHandler = inputHandler;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // 점프 초기화
        if (IsGrounded() && curJumpIndex > 0)
        {
            curJumpIndex = 0;
            inputHandler.isJump = false;
        }

        // 카메라 위치 조정
        CameraPos();
    }

    private void FixedUpdate()
    {
        Move();

        if(inputHandler.isJump && curJumpIndex == 0 && IsGrounded()) // 처음 점프
        {
            Jump();

        }
        else if(inputHandler.isDJump && 0 < curJumpIndex && curJumpIndex < maxJumpIndex && !IsWall()) // 연속 점프가 가능할 경우
        {
            Jump();
        }
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    public void Jump() // 점프
    {
        if(playerStamina.UseStamina(jumpStamina))
        {
            rb.AddForce(Vector2.up * statHandler.JumpPower, ForceMode.Impulse);
            inputHandler.isDJump = false;
            curJumpIndex++;
        }
    }

    private void Move() // 움직임
    {
        if(IsWall())
        {
            Climb();
        }
        else
        {
            rb.useGravity = true;

            Vector3 moveInput = (transform.forward * inputHandler.CurMoveInput.y + transform.right * inputHandler.CurMoveInput.x).normalized * statHandler.MoveSpeed;
            Vector3 targetPos = rb.position + moveInput * Time.fixedDeltaTime;
            rb.MovePosition(targetPos);
        }
    }

    private void CameraLook() // 마우스 입력으로 시선 처리
    {
        // 세로 회전
        camCurXRot += inputHandler.MouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // 회전 범위 제한
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        // 가로 회전
        transform.eulerAngles += new Vector3(0, inputHandler.MouseDelta.x * lookSensitivity, 0);
    }

    private bool IsGrounded() // 점프 가능 레이어 확인
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 1.05f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    private void Climb() // 벽 타기
    {
        if (playerStamina.UseStamina(climbStamina * Time.fixedDeltaTime) && playerStamina.curValue > climbStamina)
        {
            // 중력 해제로 위치 고정
            rb.useGravity = false;

            // 입력 방향 계산
            Vector3 inputDir = (transform.up * inputHandler.CurMoveInput.y + transform.right * inputHandler.CurMoveInput.x).normalized;

            //이동 속도
            float climbSpeed = statHandler.MoveSpeed - 2f;
            Vector3 targetVelocity = inputDir * climbSpeed;

            // 현재 속도와의 차이만큼 즉시 보간
            Vector3 velocityChange = targetVelocity - rb.velocity;
            rb.AddForce(velocityChange, ForceMode.VelocityChange);

            if (IsGrounded())
                rb.useGravity = true;
        }
        else
        {
            // 스태미나 없으면 떨어짐
            rb.useGravity = true;
            rb.AddForce(Vector3.down, ForceMode.VelocityChange);
        }
    }

    private bool IsWall() // 벽 타기 가능 레이어 확인
    {
        Ray[] rays = new Ray[3]
        {
            new Ray(transform.position, transform.forward),
            new Ray(transform.position + (transform.up * 0.5f), transform.forward),
            new Ray(transform.position + (-transform.up * 0.5f), transform.forward),
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.6f, wallLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    private void CameraPos() // 카메라 위치 조정
    {
        // 카메라 뒤로 Ray
        Ray ray = new Ray(cameraContainer.position, -cameraContainer.forward * cameraDistance + cameraContainer.right * 0.5f + cameraContainer.up * 1.3f);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, cameraDistance, groundLayerMask))
        {
            // Ray로 충동 시 카메라 위치 변경
            _camera.transform.position = hit.point + hit.transform.forward * 0.5f;
        }
        else
        {
            // 기본 위치
            _camera.transform.position = cameraContainer.position - cameraContainer.forward * cameraDistance + cameraContainer.right * 0.5f + cameraContainer.up * 1.3f;
        }
    }
}
