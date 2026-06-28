using NUnit.Framework;
using UnityEngine;

public class InteractDetector : MonoBehaviour
{
    [SerializeField]
    private PlayerCharacter _owner;
    [SerializeField]
    private float _detectorDistance;
    [SerializeField]
    private Vector3 _detectorBoxSize = Vector3.one;
    [SerializeField]
    private LayerMask _interactableLayer;

    private IInteractable _detectorInteractable;
    private bool _isInteracting;

    public bool Enabled {get; private set;} = true;

    public void SetEnabled(bool isEnabled)
    {
        Enabled = isEnabled;
    }

    private void Update()
    {
        updateDetection();
    }

    private void updateDetection()
    {
        if (_isInteracting)
        {
            _isInteracting = false;
            return;
        }
        if (Enabled == true)
        {        
            Transform cameraTransform = Camera.main.transform;
            bool IsDetectingInteractable = Physics.BoxCast(cameraTransform.position, 
                                                            _detectorBoxSize * 0.5f, 
                                                            cameraTransform.forward, 
                                                            out RaycastHit hit, 
                                                            Quaternion.identity, 
                                                            _detectorDistance, 
                                                            _interactableLayer
                                                            );
            if (IsDetectingInteractable)
            {
                IInteractable interactable = hit.collider.GetComponentInParent<IInteractable>();                if (interactable != null)
                {
                    _detectorInteractable = interactable;
                }
            }
            else
            {
                _detectorInteractable = null;
            }
        }
    }

    public void Interact()
    {
        if (_detectorInteractable != null && Enabled == true)
        {
            _detectorInteractable.Interact(_owner);
            _detectorInteractable = null;
            _isInteracting = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Transform cameraTransform = Camera.main.transform;
        if (Enabled == true)
        {        
            bool IsDetectingInteractable = Physics.BoxCast(cameraTransform.position, 
                                                            _detectorBoxSize * 0.5f, 
                                                            cameraTransform.forward, 
                                                            out RaycastHit hit, 
                                                            Quaternion.identity, 
                                                            _detectorDistance, 
                                                            _interactableLayer
                                                            );
            if (IsDetectingInteractable)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(cameraTransform.position, cameraTransform.position + cameraTransform.forward * hit.distance);
                Gizmos.DrawWireCube(cameraTransform.position + cameraTransform.forward * hit.distance, _detectorBoxSize);  
            }
            else
            {
                Gizmos.DrawLine(cameraTransform.position, cameraTransform.position + cameraTransform.forward * _detectorDistance);
                Gizmos.DrawWireCube(cameraTransform.position + cameraTransform.forward * _detectorDistance, _detectorBoxSize);  
            }
        }
    }
}
