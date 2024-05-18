using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesController : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemyList = new List<Enemy>();

    public Enemy SpawnEnemy(Transform generatorPosition)
    {
        Enemy enemy = Instantiate(_enemyList[0], transform.position, Quaternion.identity);
        enemy.SetTarget(generatorPosition);
        return enemy;
    }
}