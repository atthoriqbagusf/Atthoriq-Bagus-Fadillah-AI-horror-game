using UnityEngine;
using UnityEngine.UI;

public class StaminaUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _uiObject;
    [SerializeField]
    private Image _staminaFill;

    public void SetVisible(bool value)
    {
        _uiObject?.SetActive(value);
    }

    public void SetStaminaFill(float value, float maxValue)
    {
        if (_staminaFill != null)
        {
            _staminaFill.fillAmount =value / maxValue;
        }
    }
}
