using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private StaminaUI _staminaUI;
    [SerializeField]
    private BatteryLevelUi _batteryLevelUI;
    [SerializeField]
    private InteractionInfoUI _interactionInfoUI;
    [SerializeField]
    private CrosshairUI _crosshairUI;

    private static HUDManager _instance;

    public static HUDManager Instance => _instance;
    public StaminaUI StaminaUI => _staminaUI;
    public BatteryLevelUi BatteryLevelUi => _batteryLevelUI;
    public InteractionInfoUI InteractionInfoUI => _interactionInfoUI;
    public CrosshairUI CrosshairUI => _crosshairUI;

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
