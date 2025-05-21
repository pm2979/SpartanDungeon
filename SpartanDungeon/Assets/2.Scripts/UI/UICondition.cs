using UnityEngine;
using UnityEngine.UI;

public class UICondition : MonoBehaviour
{
    public Image hpBar;
    public Image staminaBar;

    private void OnEnable()
    {
        EventBus.Subscribe("HpUpdate", UpdateHp);
        EventBus.Subscribe("StaminaUpdate", UpdateStamina);
    }

    private void OnDisable()
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
