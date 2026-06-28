using UnityEngine;

public class SightPerception : MonoBehaviour
{
    [SerializeField]
    private Transform _eyePosition;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _viewDistance = 10f;
    [SerializeField]
    private float _viewAngle = 70f;
    [SerializeField]
    private LayerMask _targetLayer;

    public bool CanSeePlayer { get; private set; }
    public Vector3 LastSeenPosition { get; private set;}

    void Update()
    {
        CanSeePlayer = CheckSight();
    }

    public bool CheckSight()
    {
        if(_target == null)
        {
            return false;
        }

        float distance = Vector3.Distance(_eyePosition.position, _target.position);
        if(distance > _viewDistance)
        {
            return false;
        }

        Vector3 dirToTarget = _target.position - _eyePosition.position;
        float angle = Vector3.Angle(_eyePosition.forward, dirToTarget);

        if (angle > +_viewAngle * 0.5f)
        {
            return false;
        }

        bool isSeetarget = Physics.Raycast(_eyePosition.position, dirToTarget.normalized, out RaycastHit hit, _viewDistance, _targetLayer);
        if(isSeetarget == true)
        {
            if (hit.transform == _target)
            {
                LastSeenPosition = _target.position;
                return true;   
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        if (_eyePosition == null)
        {
            return;
        }

        Gizmos.color = Color.red;
        bool isSeeTarget = CheckSight();
        if (isSeeTarget == true)
        {
            Gizmos.color = Color.green;
        }

        Gizmos.DrawWireSphere(_eyePosition.position, _viewDistance);

        Vector3 left = Quaternion.Euler(0, -_viewAngle / 2, 0) * _eyePosition.forward;
        Vector3 right= Quaternion.Euler(0, _viewAngle / 2, 0) * _eyePosition.forward;

        Gizmos.DrawRay(_eyePosition.position, left * _viewDistance);
        Gizmos.DrawRay(_eyePosition.position, right * _viewDistance);

    }
}
