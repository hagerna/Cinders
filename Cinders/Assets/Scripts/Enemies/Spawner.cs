using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject[] Enemies;

    private void Start()
    {
        SpawnRandomEnemies(2, 4f);
    }

    private void Spawn(GameObject EnemyType)
    {
        float x = Random.Range(transform.position.x - transform.localScale.x / 2, transform.position.x + transform.localScale.x / 2);
        float y = Random.Range(transform.position.y - transform.localScale.y / 2, transform.position.y + transform.localScale.y / 2);
        float z = Random.Range(transform.position.z - transform.localScale.z / 2, transform.position.z + transform.localScale.z / 2);
        Vector3 position = new Vector3(x, y, z);
        Instantiate(EnemyType, position, Quaternion.identity);
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
