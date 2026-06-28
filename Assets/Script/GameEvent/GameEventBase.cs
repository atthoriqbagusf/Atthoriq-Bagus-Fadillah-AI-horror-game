using UnityEngine;
using UnityEngine.Events;

public abstract class GameEventBase : MonoBehaviour
{
    [SerializeField]
    private string _id;
    [SerializeField]
    private bool _isOneTime;

    public UnityEvent OnEventTriggered;
    public UnityEvent onEventFinished;

    public string ID => _id;

    public void Start()
    {
        GameEventManager.Instance.Register(this);
    }

    public virtual void Trigger()
    {
        OnEventTriggered?.Invoke();
    }

    public virtual void Finish()
    {
        onEventFinished?.Invoke();
        if (_isOneTime == true)
        {
            GameEventManager.Instance.Unregister(this);
            Destroy(gameObject);
        }
    }
}
