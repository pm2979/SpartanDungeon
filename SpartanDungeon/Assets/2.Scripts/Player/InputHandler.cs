using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Vector2 CurMoveInput { get; private set; } // 현재 입력 값
    public Vector2 MouseDelta { get; private set; }  // 마우스 변화값
    public bool isJump = false;
    public bool isDJump = false;

    public void OnLookInput(InputAction.CallbackContext context) // 마우스 입력 처리
    {
        MouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)  // 이동(wasd) 입력 처리
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

    public void OnJumpInput(InputAction.CallbackContext context) // 점프(space) 입력 처리
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

    public void OnItemUseInput(InputAction.CallbackContext context) // 아이템 사용 입력 처리
    {
        if (context.phase == InputActionPhase.Started)
        {
            EventBus.Publish("UseItem", null);
        }
    }
}
