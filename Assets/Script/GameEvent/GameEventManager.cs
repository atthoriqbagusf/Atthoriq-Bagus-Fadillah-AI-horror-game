using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    private static GameEventManager _instance;

    private Dictionary<string, GameEventBase> _gameEvents = new Dictionary<string, GameEventBase>();

    public static GameEventManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    public void Register(GameEventBase gameEvent)
    {
        if (_gameEvents.ContainsKey(gameEvent.ID) == false)
        {
            _gameEvents.Add(gameEvent.ID, gameEvent);
        }
    }

    public void Unregister(GameEventBase gameEvent)
    {
        if (_gameEvents.ContainsKey(gameEvent.ID) == true)
        {
            _gameEvents.Remove(gameEvent.ID);
        }
    }

    public void TriggerEvent(string id)
    {
        bool isGameEventFound = _gameEvents.TryGetValue(id, out GameEventBase gameEvent);
        if (isGameEventFound == true)
        {
            gameEvent.Trigger();
        }
    }

    public void FinishEvent(string id)
    {
        bool isGameEventFound = _gameEvents.TryGetValue(id, out GameEventBase gameEvent);
        if (isGameEventFound == true)
        {
            gameEvent.Finish();
        }
    }
}
