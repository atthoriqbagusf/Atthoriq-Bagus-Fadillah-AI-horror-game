using UnityEngine;

public class DisplayCursor : MonoBehaviour
{
    public void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
