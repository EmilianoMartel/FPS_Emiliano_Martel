using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _waveTime = 10;
    [SerializeField] private int _enemyPerWave = 10;
    [SerializeField] private HealthPoints _generatorLife;
    [SerializeField] private Transform _generatorPosition;
    [SerializeField] private List<SpawnEnemiesController> _spawnList = new List<SpawnEnemiesController>();
    [SerializeField] private float _timeBetweenSpawns = 1;
    [Header("GameOver data")]
    [SerializeField] private BoolDataSO _winData;
    [SerializeField] private StringChannel _menuNameEvent;
    [SerializeField] private BoolChanelSo _startedGame;
    [SerializeField] private string _finalSceneName = "FinalScene";
    private List<Enemy> _enemyList = new();
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
        Validate();
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
            int spawnIndex = RandomIndexSpawn();

            Enemy enemy = _spawnList[spawnIndex].SpawnEnemy(_generatorPosition);
            if (enemy.TryGetComponent<HealthPoints>(out HealthPoints hp))
            {
                hp.dead += HandleEnemiesDie;
            }
            enemy.transform.parent = transform;
            _enemyList.Add(enemy);
        }
    }

    private void HandleEnemiesDie()
    {
        _enemiesDie++;
        
        if (_enemiesDie >= _enemyPerWave)
        {
            WinOrLoseLogic(true);
        }
    }

    private void HandleGeneratorDie()
    {
        WinOrLoseLogic(false);
    }

    private void WinOrLoseLogic(bool isWinning)
    {
        _winData.boolData = isWinning;
        _menuNameEvent.InvokeEvent(_finalSceneName);
        _startedGame.InvokeEvent(false);
    }

    private int RandomIndexSpawn()
    {
        return Random.Range(0, _spawnList.Count);
    }

    private void Validate()
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
        if (_spawnList.Count == 0)
        {
            Debug.LogError($"{name}: Spawn is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_winData)
        {
            Debug.LogError($"{name}: WinData is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
    }
}