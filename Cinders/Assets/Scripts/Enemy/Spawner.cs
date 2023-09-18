using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] Enemies;

    private void Start()
    {
        SpawnRandomEnemies(2, 8f);
    }

    private void Spawn(GameObject EnemyPrefab)
    {
        Bounds bounds = GetComponent<Collider>().bounds;
        Vector3 position = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
        transform.TransformPoint(position);
        Instantiate(EnemyPrefab, position, Quaternion.identity);
    }


    public void SpawnEnemies(string enemyType, int enemyNumber = 1, float spawnDelay = 0)
    {
        foreach (GameObject enemy in Enemies)
        {
            if (enemy.name == enemyType)
            {
                StartCoroutine(SpawnCycle(enemy, enemyNumber, spawnDelay));
            }
        }
    }

    public void SpawnRandomEnemies(int enemyNumber = 1, float spawnDelay = 0)
    {
        StartCoroutine(RandomCycle(enemyNumber, spawnDelay));
    }

    IEnumerator SpawnCycle(GameObject EnemyType, int enemyNumber, float spawnDelay)
    {
        for (int i = 0; i < enemyNumber; i++)
        {
            Spawn(EnemyType);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    IEnumerator RandomCycle(int enemyNumber, float spawnDelay)
    {
        for (int i = 0; i < enemyNumber; i++)
        {
            Spawn(Enemies[Random.Range(0, Enemies.Length)]);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
