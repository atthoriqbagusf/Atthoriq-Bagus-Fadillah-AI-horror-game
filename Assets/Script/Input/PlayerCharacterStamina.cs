using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCharacterStamina : MonoBehaviour
{
    [SerializeField]
    private PlayerCharacterMovement _characterMovement;
    [SerializeField]
    private float _maxStamina = 100;
    [SerializeField]
    private float _sprintStaminaCost = 20;
    [SerializeField]
    private float _staminaRegenValue = 20;

    private float _currentStamina;
    private Coroutine _stopRegenStaminaCorourine;
    private bool _isWaitingRegenStamina;

    void Awake()
    {
        _currentStamina = _maxStamina;
    }

    void Update()
    {
        CalculateStamina();
    }

    public void CalculateStamina()
    {
        if (_characterMovement.IsSprint)
        {
            if (_stopRegenStaminaCorourine != null)
            {
                StopCoroutine(_stopRegenStaminaCorourine);
                _stopRegenStaminaCorourine = null;
            }
            _isWaitingRegenStamina = false;

            if (_currentStamina > 0)
            {
                _currentStamina = _currentStamina - _sprintStaminaCost * Time.deltaTime;
            }
            else
            {
                _characterMovement.SetSprint(false);
            }
        }
        else
        {
            if (_currentStamina < _maxStamina)
            {
                _currentStamina = _currentStamina + _staminaRegenValue * Time.deltaTime;
            }
            else if(_isWaitingRegenStamina == false)
            {
                _stopRegenStaminaCorourine = StartCoroutine(StopRegenStaminaWait());
                _isWaitingRegenStamina = true;
            }
        }
        _currentStamina = Mathf.Clamp(_currentStamina,0,_maxStamina);
    }

    private IEnumerator StopRegenStaminaWait()
    {
        yield return new WaitForSeconds(1f);

    }
}
