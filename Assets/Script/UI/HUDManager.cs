using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private StaminaUI _staminaUI;
    [SerializeField]
    private BatteryLevelUi _batteryLevelUI;

    private static HUDManager _instance;

    public static HUDManager Instance => _instance;
    public StaminaUI StaminaUI => _staminaUI;
    public BatteryLevelUi BatteryLevelUi => _batteryLevelUI;

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }
}
