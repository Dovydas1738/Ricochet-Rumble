using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private GameObject enemyPrefab;

    public GameObject SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoints[Random.Range(0, spawnPoints.Count - 1)]);
        return newEnemy;
    }
}
