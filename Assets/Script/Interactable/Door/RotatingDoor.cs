using System.Collections;
using UnityEngine;

public class RotatingDoor : Door
{
    [SerializeField]
    private float _openAngle;
    [SerializeField]
    private float _closedAngle;

    public override void Open()
    {
        if (_animatingDoorCoroutine != null)
        {
            StopCoroutine(_animatingDoorCoroutine);
        }
        _animatingDoorCoroutine = StartCoroutine(RotateDoor(_openAngle));
        base.Open();
    }

    public override void Close()
    {
        if (_animatingDoorCoroutine != null)
        {
            StopCoroutine(_animatingDoorCoroutine);
        }
        _animatingDoorCoroutine = StartCoroutine(RotateDoor(_closedAngle));
        base.Close();
    }

    private IEnumerator RotateDoor(float targetAngle)
    {
        _isAnimating = true;
        float startAngle = _doorTransform.localEulerAngles.y;
        float time = 0;

        while (time < _duration)
        {
            time = time + Time.deltaTime;
            float angle = Mathf.LerpAngle(startAngle, targetAngle, time / _duration);
            _doorTransform.localRotation = Quaternion.Euler(0f, angle, 0f);
            yield return null;
        }

        _doorTransform.localRotation = Quaternion.Euler(0f, targetAngle, 0f);
        _isAnimating = false;
    }
}
