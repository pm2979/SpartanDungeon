using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    [Header("Move Setting")]
    private Vector2 curMoveInput; // 현재 입력 값
    public LayerMask groundLayerMask; // 점프 가능한 레이어
    public LayerMask wallLayerMask; // 벽 타기 가능한 레이어


    [Header("Look Setting")]
    public Transform cameraContainer;
    public float minXLook;  // 최소 시야각
    public float maxXLook;  // 최대 시야각
    private float camCurXRot;
    public float lookSensitivity; // 카메라 민감도
    public Camera _camera;
    public float cameraDistance = 5.5f; // 카메라 거리

    private Vector2 mouseDelta;  // 마우스 변화값
    private PlayerStamina playerStamina;
    private Rigidbody rb;
    private StatHandler statHandler;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerStamina = GetComponent<PlayerStamina>();
        statHandler = GetComponent<StatHandler>();
    }

    private void Update()
    {
        CameraPos();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    public void OnLookInput(InputAction.CallbackContext context) // 마우스 입력 처리
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)  // 이동(wasd) 입력 처리
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMoveInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMoveInput = Vector2.zero;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context) // 점프(space) 입력 처리
    {
        if (context.phase == InputActionPhase.Started && IsGrounded() && playerStamina.UseStamina(25))
        {
            rb.AddForce(Vector2.up * statHandler.JumpPower, ForceMode.Impulse);
        }
    }

    public void OnItemUseInput(InputAction.CallbackContext context) // 아이템 사용 입력 처리
    {
        if (context.phase == InputActionPhase.Started)
        {
            EventBus.Publish("UseItem", null);
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
            Vector3 moveInput = (transform.forward * curMoveInput.y + transform.right * curMoveInput.x) * statHandler.MoveSpeed;
            Vector3 targetPos = rb.position + moveInput * Time.fixedDeltaTime;
            rb.MovePosition(targetPos);
        }

    }

    private void CameraLook() // 마우스 입력으로 시선 처리
    {
        // 세로 회전
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // 회전 범위 제한
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        // 가로 회전
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    private bool IsGrounded() // 점프 가능 레이어 확인
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 1.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    private void Climb() // 벽 타기
    {
        if(!IsWall()) return;

        if (playerStamina.UseStamina(10 * Time.fixedDeltaTime))
        {
            Debug.Log("벽타기");
        }
    }


    private bool IsWall() // 벽 타기 가능 레이어 확인
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, 0.6f, wallLayerMask))
        {
            return true;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * 0.6f);
    }
}
