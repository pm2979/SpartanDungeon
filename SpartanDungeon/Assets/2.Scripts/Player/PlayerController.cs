using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    [Header("Move Setting")]
    private Vector2 curMoveInput; // ���� �Է� ��
    public LayerMask groundLayerMask; // ���� ������ ���̾�
    public LayerMask wallLayerMask; // �� Ÿ�� ������ ���̾�


    [Header("Look Setting")]
    public Transform cameraContainer;
    public float minXLook;  // �ּ� �þ߰�
    public float maxXLook;  // �ִ� �þ߰�
    private float camCurXRot;
    public float lookSensitivity; // ī�޶� �ΰ���
    public Camera _camera;
    public float cameraDistance = 5.5f; // ī�޶� �Ÿ�

    private Vector2 mouseDelta;  // ���콺 ��ȭ��
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

    public void OnLookInput(InputAction.CallbackContext context) // ���콺 �Է� ó��
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)  // �̵�(wasd) �Է� ó��
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

    public void OnJumpInput(InputAction.CallbackContext context) // ����(space) �Է� ó��
    {
        if (context.phase == InputActionPhase.Started && IsGrounded() && playerStamina.UseStamina(25))
        {
            rb.AddForce(Vector2.up * statHandler.JumpPower, ForceMode.Impulse);
        }
    }

    public void OnItemUseInput(InputAction.CallbackContext context) // ������ ��� �Է� ó��
    {
        if (context.phase == InputActionPhase.Started)
        {
            EventBus.Publish("UseItem", null);
        }
    }

    private void Move() // ������
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

    private void CameraLook() // ���콺 �Է����� �ü� ó��
    {
        // ���� ȸ��
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // ȸ�� ���� ����
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        // ���� ȸ��
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    private bool IsGrounded() // ���� ���� ���̾� Ȯ��
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

    private void Climb() // �� Ÿ��
    {
        if(!IsWall()) return;

        if (playerStamina.UseStamina(10 * Time.fixedDeltaTime))
        {
            Debug.Log("��Ÿ��");
        }
    }


    private bool IsWall() // �� Ÿ�� ���� ���̾� Ȯ��
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, 0.6f, wallLayerMask))
        {
            return true;
        }

        return false;
    }

    private void CameraPos() // ī�޶� ��ġ ����
    {
        // ī�޶� �ڷ� Ray
        Ray ray = new Ray(cameraContainer.position, -cameraContainer.forward * cameraDistance + cameraContainer.right * 0.5f + cameraContainer.up * 1.3f);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, cameraDistance, groundLayerMask))
        {
            // Ray�� �浿 �� ī�޶� ��ġ ����
            _camera.transform.position = hit.point + hit.transform.forward * 0.5f;
        }
        else
        {
            // �⺻ ��ġ
            _camera.transform.position = cameraContainer.position - cameraContainer.forward * cameraDistance + cameraContainer.right * 0.5f + cameraContainer.up * 1.3f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * 0.6f);
    }
}
