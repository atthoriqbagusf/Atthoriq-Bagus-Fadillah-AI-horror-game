using UnityEngine;
using UnityEngine.UI;

public class BatteryLevelUi : MonoBehaviour
{
    [SerializeField]
    private GameObject _uiObject;
    [SerializeField]
    private Color _highColor = Color.white;
    [SerializeField]
    private Color _mediumColor = Color.white;
    [SerializeField]
    private Color _lowColor = Color.white;
    [SerializeField]
    private Image _batteryFill;

    public void SetVisibillity(bool value)
    {
        _uiObject?.SetActive(value);
    }

    public void UpdateBatteryUI(float value, float maxValue)
    {
        float fillAmount = value / maxValue;
        _batteryFill.fillAmount = fillAmount;
        Color color = _highColor;
        if (fillAmount < 0.25f)
        {
            color = _lowColor;
        }
        else if (fillAmount > 0.25f && fillAmount < 0.5f)
        {
            color = _mediumColor;
        }

        _batteryFill.color = color;
    } 
}
