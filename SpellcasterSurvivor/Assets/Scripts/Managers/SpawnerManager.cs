using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private List<Vector3> _spawnPointList;
    [SerializeField] private int _spawnScale = 10;
    [SerializeField] private float _scaleOfWave = 10;
    [SerializeField] private float _secondsBetweenWave = 1;


    private int _numberOfEnemyToSpawn;
    private int _numberOfEnemyCurrentlySpawned;
    private int _numberOfEnemyKilled;

    [SerializeField] private GameObject _enemyPrefab;
    private int _enemyHp;

    private float _timer;

    private Transform _target;
    private StateManager _stateManager;

    public void StartSpawner(int stage, Transform target, StateManager stateManager)
    {
        _numberOfEnemyToSpawn = _spawnScale * (stage + stage/10);
        _numberOfEnemyCurrentlySpawned = 0;
        _numberOfEnemyKilled = 0;

        _timer = 0;
        _enemyHp = (stage * (1 + (stage / 10)));
        _target = target;
        _stateManager = stateManager;
    }

    private void Update()
    {
        if (_numberOfEnemyCurrentlySpawned >= _numberOfEnemyToSpawn)
            return;

        _timer += Time.deltaTime;

        if (_timer >= _secondsBetweenWave)
        {
            Spawn();
            _timer = 0;
        }
    }

    private void Spawn()
    {
        for (int i = 0; i < _scaleOfWave; i++)
        {
            int random = Random.Range(0, _spawnPointList.Count);
            Vector3 position = new Vector3(_spawnPointList[random].x + (i*2), _spawnPointList[random].y, _spawnPointList[random].z + (i*2));
            GameObject enemy = Instantiate(_enemyPrefab, position, Quaternion.identity);
            enemy.GetComponent<Enemy>().SetData(_enemyHp, this);
            enemy.GetComponent<EnemyMovement>().SetTarget(_target);
            _numberOfEnemyCurrentlySpawned++;
        }
    }

    public void EnemyDie()
    {
        _numberOfEnemyKilled++;

        if (_numberOfEnemyKilled >= _numberOfEnemyToSpawn)
        {
            _stateManager.Victory();
        }
    }

    public int GetNumberOfEnemyToKill()
    {
        return _numberOfEnemyToSpawn - _numberOfEnemyKilled;
    }
}
