using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Vector2 CurMoveInput { get; private set; } // ���� �Է� ��
    public Vector2 MouseDelta { get; private set; }  // ���콺 ��ȭ��
    public bool isJump = false;
    public bool isDJump = false;

    public void OnLookInput(InputAction.CallbackContext context) // ���콺 �Է� ó��
    {
        MouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)  // �̵�(wasd) �Է� ó��
    {
        if (context.phase == InputActionPhase.Performed)
        {
            CurMoveInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            CurMoveInput = Vector2.zero;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context) // ����(space) �Է� ó��
    {
        if (context.phase == InputActionPhase.Started)
        {
            if(isJump)
            {
                isDJump = true;
            }

            isJump = true;

        }
    }

    public void OnItemUseInput(InputAction.CallbackContext context) // ������ ��� �Է� ó��
    {
        if (context.phase == InputActionPhase.Started)
        {
            EventBus.Publish("UseItem", null);
        }
    }
}
