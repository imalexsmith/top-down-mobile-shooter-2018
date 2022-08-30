using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    // ==============================================================================================
    public float SpawnMinDelay;
    public float SpawnMaxDelay;
    public Enemy EnemyPrefab;

    private readonly List<Enemy> _enemies = new List<Enemy>();
    private float _spawnCurrentDelay;
    private float _lastSpawnTime = -1f;
    

    // ==============================================================================================
    public void Spawn()
    {
        if (Time.time - _lastSpawnTime > _spawnCurrentDelay)
        {
            _spawnCurrentDelay = Random.Range(SpawnMinDelay, SpawnMaxDelay);

            SpawnImmediately();
        }
    }

    public void SpawnImmediately()
    {
        if (EnemyPrefab != null)
        {
            _lastSpawnTime = Time.time;

            Enemy newEnemy = null;

            for (int i = 0; i < _enemies.Count; i++)
            {
                if (!_enemies[i].isActiveAndEnabled)
                {
                    newEnemy = _enemies[i];
                    break;
                }
            }

            if (newEnemy == null)
            {
                newEnemy = Instantiate(EnemyPrefab);
                newEnemy.gameObject.name = Random.Range(0, 10000).ToString();
                _enemies.Add(newEnemy);
            }

            newEnemy.gameObject.SetActive(true);
            newEnemy.transform.position = transform.position;
            newEnemy.NewRandomTarget();
        }
    }

    protected virtual void Awake()
    {
        _spawnCurrentDelay = Random.Range(SpawnMinDelay, SpawnMaxDelay);
    }

    protected virtual void Update()
    {
        Spawn();
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos() { }
#endif
}
