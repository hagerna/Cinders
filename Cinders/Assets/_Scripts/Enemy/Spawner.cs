using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] float StartDelay;
    [SerializeField] GameObject[] enemies;

    private void Start()
    {
        //SpawnRandomEnemies(3, 8f);
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

    private IEnumerator SpawnCycle(GameObject EnemyType, int enemyNumber, float spawnDelay)
    {
        yield return new WaitForSeconds(StartDelay);
        for (int i = 0; i < enemyNumber; i++)
        {
            Spawn(EnemyType);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private IEnumerator RandomCycle(int enemyNumber, float spawnDelay)
    {
        yield return new WaitForSeconds(StartDelay);
        for (int i = 0; i < enemyNumber; i++)
        {
            Spawn(enemies[Random.Range(0, enemies.Length)]);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    /// <summary>
    /// Spawn Enemies of a given type with optional arguments for Additional numbers and a delay between spawns.
    /// </summary>
    /// <param name="enemyType">The name of the enemy to be spawned: Ghost, HeavyGhost, MageGhost. </param>
    /// <param name="enemyNumber">The number of enemies of that type to spawn. </param>
    /// <param name="spawnDelay">If spawning multiple enemies, the delay between each being spawned. </param>
    public void SpawnEnemies(string enemyType, int enemyNumber = 1, float spawnDelay = 0)
    {
        foreach (GameObject enemy in enemies)
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

    public void SetStartDelay(float delay)
    {
        StartDelay = delay;
    }
}
