using UnityEngine;
using UnityEngine.UI;

public class UICondition : MonoBehaviour
{
    public Image hpBar;
    public Image staminaBar;

    private void OnEnable() // UI 상태 변화 구독
    {
        EventBus.Subscribe("HpUpdate", UpdateHp);
        EventBus.Subscribe("StaminaUpdate", UpdateStamina);
    }

    private void OnDisable() // 구독 해제
    {
        EventBus.Unsubscribe("HpUpdate", UpdateHp);
        EventBus.Unsubscribe("StaminaUpdate", UpdateStamina);
    }

    private void UpdateHp(object amount)
    {
        hpBar.fillAmount = (float) amount;
    }

    private void UpdateStamina(object amount)
    {
        staminaBar.fillAmount = (float) amount;
    }

}
