using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovingGhost : MonoBehaviour
{
    [SerializeField]
    private List<Vector3> _destinations = new List<Vector3>();
    [SerializeField]
    private float _speed = 1;
    [SerializeField]
    private float _distanceTolerance = 0.2f;
    [SerializeField]
    private bool _autoNextDestination;
    [SerializeField]
    private bool _playOnAwake = true;

    public UnityEvent OnStartMoving;
    public UnityEvent OnReachDestination;
    public UnityEvent OnReachAllDestination;

    private int _destinationIndex;
    private Coroutine _moveCouroutine;

    private void Start()
    {
        if (_playOnAwake == true)
        {
            MoveToNextDestination();
        }
    }

    public void MoveToNextDestination()
    {
        if (_destinations.Count > 0 && _destinations.Count > _destinationIndex)
        {
            if (_moveCouroutine != null)
            {
                StopCoroutine(_moveCouroutine);
                _moveCouroutine = null;
            }
            OnStartMoving?.Invoke();
            _moveCouroutine = StartCoroutine(MoveToTarget(_destinations[_destinationIndex]));
            _destinationIndex = _destinationIndex + 1;
        }
        else
        {
            OnReachAllDestination?.Invoke();
            Destroy(this);
        }
    }

    public void RotateToDestination()
    {
        if (_destinations.Count > 0 && _destinations.Count > _destinationIndex)
        {
            transform.LookAt(_destinations[_destinationIndex]);
        }
    }

    private IEnumerator MoveToTarget(Vector3 target)
    {
        RotateToDestination();

        while (Vector3.Distance(transform.position, target) > _distanceTolerance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
        OnReachDestination?.Invoke();
        if (_autoNextDestination == true)
        {
            MoveToNextDestination();
        }
        else
        { 
            if (_destinationIndex >= _destinations.Count)
            {
                OnReachAllDestination?.Invoke();
                Destroy(this);
            }
        }
            
    }
}
