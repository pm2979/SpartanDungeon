using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move Setting")]
    public LayerMask groundLayerMask; // ���� ������ ���̾�
    public LayerMask wallLayerMask; // �� Ÿ�� ������ ���̾�
    public int jumpStamina;
    public int climbStamina;
    public int curJumpIndex = 0;
    public int maxJumpIndex = 1;

    [Header("Look Setting")]
    public Transform cameraContainer;
    public float minXLook;  // �ּ� �þ߰�
    public float maxXLook;  // �ִ� �þ߰�
    private float camCurXRot;
    public float lookSensitivity; // ī�޶� �ΰ���
    public Camera _camera;
    public float cameraDistance = 5.5f; // ī�޶� �Ÿ�

    private PlayerStamina playerStamina;
    private StatHandler statHandler;
    private InputHandler inputHandler;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerStamina = GetComponent<PlayerStamina>();
        statHandler = GetComponent<StatHandler>();
        inputHandler = GetComponent<InputHandler>();
    }

    private void Update()
    {
        // ���� �ʱ�ȭ
        if (IsGrounded() && curJumpIndex > 0)
        {
            curJumpIndex = 0;
            inputHandler.isJump = false;
        }

        // ī�޶� ��ġ ����
        CameraPos();
    }

    private void FixedUpdate()
    {
        Move();

        if(inputHandler.isJump && curJumpIndex == 0 && IsGrounded()) // ó�� ����
        {
            Jump();
        }
        else if(inputHandler.isJump && 0 < curJumpIndex && curJumpIndex < maxJumpIndex && !IsWall()) // ���� ������ ������ ���
        {
            Jump();
        }
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    public void Jump() // ����
    {
        if(playerStamina.UseStamina(jumpStamina))
        {
            rb.AddForce(Vector2.up * statHandler.JumpPower, ForceMode.Impulse);
            curJumpIndex++;
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
            rb.useGravity = true;

            Vector3 moveInput = (transform.forward * inputHandler.CurMoveInput.y + transform.right * inputHandler.CurMoveInput.x).normalized * statHandler.MoveSpeed;
            Vector3 targetPos = rb.position + moveInput * Time.fixedDeltaTime;
            rb.MovePosition(targetPos);
        }
    }

    private void CameraLook() // ���콺 �Է����� �ü� ó��
    {
        // ���� ȸ��
        camCurXRot += inputHandler.MouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // ȸ�� ���� ����
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        // ���� ȸ��
        transform.eulerAngles += new Vector3(0, inputHandler.MouseDelta.x * lookSensitivity, 0);
    }

    private bool IsGrounded() // ���� ���� ���̾� Ȯ��
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

    private void Climb() // �� Ÿ��
    {
        if (playerStamina.UseStamina(climbStamina * Time.fixedDeltaTime) && playerStamina.curValue > climbStamina)
        {
            // �߷� ������ ��ġ ����
            rb.useGravity = false;

            // �Է� ���� ���
            Vector3 inputDir = (transform.up * inputHandler.CurMoveInput.y + transform.right * inputHandler.CurMoveInput.x).normalized;

            //�̵� �ӵ�
            float climbSpeed = statHandler.MoveSpeed - 2f;
            Vector3 targetVelocity = inputDir * climbSpeed;

            // ���� �ӵ����� ���̸�ŭ ��� ����(Impulse)
            Vector3 velocityChange = targetVelocity - rb.velocity;
            rb.AddForce(velocityChange, ForceMode.VelocityChange);

            if (IsGrounded())
                rb.useGravity = true;
        }
        else
        {
            // ���¹̳� ������ ������
            rb.useGravity = true;
            rb.AddForce(Vector3.down, ForceMode.VelocityChange);
        }
    }

    private bool IsWall() // �� Ÿ�� ���� ���̾� Ȯ��
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
}
