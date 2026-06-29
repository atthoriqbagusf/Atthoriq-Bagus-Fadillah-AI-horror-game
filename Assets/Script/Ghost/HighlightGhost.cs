using UnityEngine;
using UnityEngine.Events;

public class HighlightGhost : MonoBehaviour
{
    [SerializeField]
    private float _maxDistance = 10;
    [SerializeField]
    private float _dotTreshold = 0.8f;
    [SerializeField]
    private bool _autoActive;

    public UnityEvent OnSeeGhost;

    private bool _isActive;

    private void Awake()
    {
        _isActive = _autoActive;
    }

    public void SetActive(bool value)
    {
        _isActive = value;
    }

    private bool CheckIsPlayerSeeGhost()
    {
        Transform playerCamera = Camera.main.transform;
        Vector3 ghostDirection = (transform.position - playerCamera.position).normalized;
        float dotResult = Vector3.Dot(playerCamera.forward, ghostDirection);
        if (dotResult > _dotTreshold)
        {
            float distance = Vector3.Distance(playerCamera.position, transform.position);
            if (distance < _maxDistance)
            {
                return true;
            }
        }
        return false;
    }

    private void Update()
    {
        if (_isActive)
        {
            bool isPlayerSeeGhost = CheckIsPlayerSeeGhost();
            if(isPlayerSeeGhost == true)
            {
                OnSeeGhost?.Invoke();
                Destroy(this);
            }
        }
    }
}
