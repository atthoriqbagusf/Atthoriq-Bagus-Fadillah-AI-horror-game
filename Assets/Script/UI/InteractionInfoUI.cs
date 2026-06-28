using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionInfoUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _uiObject;
    [SerializeField]
    private TMP_Text _nameText;

    public void SetVisible(bool value)
    {
        _uiObject.SetActive(value);
    }

    public void SetNameText(string text)
    {
        _nameText.text = text;
        Canvas.ForceUpdateCanvases();
        LayoutRebuilder.ForceRebuildLayoutImmediate(_uiObject.GetComponent<RectTransform>());
        Canvas.ForceUpdateCanvases();
    }
}
