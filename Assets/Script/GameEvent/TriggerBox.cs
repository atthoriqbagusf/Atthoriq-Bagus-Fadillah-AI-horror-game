using UnityEngine;
using UnityEngine.Events;

public class TriggerBox : MonoBehaviour
{
    [SerializeField]
    private bool _autoActive;
    [SerializeField]
    private string _tag;
    [SerializeField]
    private bool _isOneTime;

    public UnityEvent OnTrigger;

    private bool _isActive;

    void Awake()
    {
        _isActive = _autoActive;
    }

    public void SetActive(bool value)
    {
        _isActive = value;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tag) && _isActive == true)
        {
            OnTrigger?.Invoke();
            if (_isOneTime == true)
            {
                Destroy(gameObject);
            }
        }
    }
}
