using UnityEngine;
using UnityEngine.UI;

public class CrosshairUI : MonoBehaviour
{
    [SerializeField]
    private Color _normalColor = Color .white;
    [SerializeField]
    private Color _highlightColor = Color.white;
    [SerializeField]
    private Image _crosshairImage;

    void Awake()
    {
        SetHighlight(false);
    }

    public void SetHighlight(bool value)
    {
        if (value == true)
        {
            _crosshairImage.color = _highlightColor;
        }
        else
        {
            _crosshairImage.color = _normalColor;
        }
    }
}
