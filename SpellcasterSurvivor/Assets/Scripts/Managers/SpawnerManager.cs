using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [Header("SpawningData")]
    [SerializeField] private List<Vector3> _spawnPointList;
    [SerializeField] private int _spawnScale = 10;
    [SerializeField] private float _scaleOfWave = 10;
    [SerializeField] private float _secondsBetweenWave = 1;

    [Header("PercentageForSpawning")]
    [SerializeField] AnimationCurve _spawnCurve;
    [SerializeField] int _fastEnemyValue;


    private int _numberOfEnemyToSpawn;
    private int _numberOfEnemyCurrentlySpawned;
    private int _numberOfEnemyKilled;

    [Header("Prefabs")]
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _fastEnemyPrefab;
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
            // Determine spawn point
            int randomSpawnPoint = Random.Range(0, _spawnPointList.Count);
            Vector3 position = new Vector3(_spawnPointList[randomSpawnPoint].x + (i*2), _spawnPointList[randomSpawnPoint].y, _spawnPointList[randomSpawnPoint].z + (i*2));

            // Determine enemy to spawn & spawn the right one
            float randomSpawnEnemy = Random.value;
            int enemyToSpawn = (int) _spawnCurve.Evaluate(randomSpawnEnemy);

            if (_fastEnemyValue == enemyToSpawn)
            {
                InstantiateEnemy(_fastEnemyPrefab, position);
            }
            else
            {
                InstantiateEnemy(_enemyPrefab, position);
            }

            // Count Enemy spawned
            _numberOfEnemyCurrentlySpawned++;
        }
    }

    private void InstantiateEnemy(GameObject prefab, Vector3 position)
    {
        GameObject enemy = Instantiate(prefab, position, Quaternion.identity);
        enemy.GetComponent<Enemy>().SetData(_enemyHp, this);
        enemy.GetComponent<EnemyMovement>().SetTarget(_target);
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
