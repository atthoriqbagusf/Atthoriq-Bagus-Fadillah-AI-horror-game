using System.Collections;
using UnityEngine;

public class SlidingDoor : Door
{
    [SerializeField]
    private Vector3 _openPosition;
    [SerializeField]
    private Vector3 _closedPosition;

    public override void Open()
    {
        if (_animatingDoorCoroutine != null)
        {
            StopCoroutine(_animatingDoorCoroutine);
        }
        _animatingDoorCoroutine = StartCoroutine(slideDoor(_openPosition));
        base.Open();
    }

    public override void Close()
    {
        if (_animatingDoorCoroutine != null)
        {
            StopCoroutine(_animatingDoorCoroutine);
        }
        _animatingDoorCoroutine = StartCoroutine(slideDoor(_closedPosition));
        base.Close();
    }

    private IEnumerator slideDoor(Vector3 targetPosition)
    {
        _isAnimating = true;
        Vector3 startPosition = _doorTransform.localPosition;
        float time = 0f;

        while (time < _duration)
        {
            time += Time.deltaTime;
            Vector3 position = Vector3.Lerp(startPosition, targetPosition, time / _duration);
            _doorTransform.localPosition = position;
            yield return null;
        }

        _doorTransform.localPosition = targetPosition;
        _isAnimating = false;
    }
}
