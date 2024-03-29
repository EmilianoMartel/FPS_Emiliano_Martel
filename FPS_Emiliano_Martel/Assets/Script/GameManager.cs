using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _waveTime = 10;
    [SerializeField] private int _enemyPerWave = 10;
    [SerializeField] private List<Enemy> _enemyListPrefab = new List<Enemy>();
    [SerializeField] private HealthPoints _generatorLife;
    [SerializeField] private Transform _generatorPosition;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _timeBetweenSpawns = 1;

    private List<Enemy> _enemyList = new List<Enemy>();
    private int _enemiesDie = 0;

    private void OnEnable()
    {
        _generatorLife.dead += HandleGeneratorDie;    
    }

    private void OnDisable()
    {
        _generatorLife.dead -= HandleGeneratorDie;
    }

    private void Awake()
    {
        if (!_generatorPosition)
        {
            Debug.LogError($"{name}: Generator position is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_generatorLife)
        {
            Debug.LogError($"{name}: Generator life is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_spawnPosition)
        {
            Debug.LogError($"{name}: Spawn is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        StartCoroutine(WaveStart());
    }

    private IEnumerator WaveStart()
    {
        yield return new WaitForSeconds( _waveTime );
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        for (int i = 0; i < _enemyPerWave; i++)
        {
            yield return new WaitForSeconds(_timeBetweenSpawns);
            Enemy enemy = Instantiate(_enemyListPrefab[0],_spawnPosition.transform.position,Quaternion.identity);
            enemy.target = _generatorPosition;
            if (enemy.TryGetComponent<HealthPoints>(out HealthPoints hp))
            {
                hp.dead += HandleEnemiesDie;
            }
            _enemyList.Add(enemy);
        }
    }

    private void HandleEnemiesDie()
    {
        _enemiesDie++;
        
        if (_enemiesDie >= _enemyPerWave)
        {
            Debug.Log("You win");
        }
    }

    private void HandleGeneratorDie()
    {
        Debug.Log("You loose");
    }
}
