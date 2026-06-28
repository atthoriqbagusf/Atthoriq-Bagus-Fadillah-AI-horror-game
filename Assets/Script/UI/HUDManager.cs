using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private StaminaUI _staminaUI;

    private static HUDManager _instance;

    public static HUDManager Instance => _instance;
    public StaminaUI StaminaUI => _staminaUI;

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
