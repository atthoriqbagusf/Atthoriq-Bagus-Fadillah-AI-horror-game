using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField]
    private GhostAIController _aiController;
    [SerializeField]
    private float _minSpawnDelay = 5f;
    [SerializeField]
    private float _maxSpawnDelay = 8f;
    [SerializeField]
    private float _minSpawnDistance = 3f;
    [SerializeField]
    private float _maxSpawnDistance = 5f;

    private Coroutine _spawnCoroutine;

    public void RestartSpawn()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
        }
        _spawnCoroutine = StartCoroutine(StartSpawn());
    }

    public IEnumerator StartSpawn()
    {
        float spawnDelay = Random.Range(_minSpawnDelay, _maxSpawnDelay);
        yield return new WaitForSeconds(spawnDelay);

        if (_aiController.Target == null || _aiController.Target.IsHiding)
        {
            RestartSpawn();
            yield break;
        }

        SpawnGhost();    
    }

    public void SpawnGhost()
    {
        float spawnDistance = Random.Range(_minSpawnDistance, _maxSpawnDistance);
        Vector3 spawnPos = _aiController.Target.transform.position - _aiController.Target.transform.forward * spawnDistance; 
        spawnPos.y = _aiController.transform.position.y;
        
        _aiController.NavMeshAgent.enabled = true;
        _aiController.NavMeshAgent.Warp(spawnPos);
        _aiController.transform.LookAt(_aiController.Target.transform);

        _aiController.gameObject.SetActive(true);

        _aiController.BehaviorGraphAgent.SetVariableValue("LastSeenPosition", _aiController.Target.transform.position);
        _aiController.BehaviorGraphAgent.enabled = true;
    }
}
