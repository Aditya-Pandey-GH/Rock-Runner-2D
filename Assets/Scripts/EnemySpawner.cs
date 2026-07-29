using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    // Variables
    [SerializeField] GameObject paperEnemy;
    [SerializeField] float spawnDelay = 3f;
    [SerializeField] float minSpawnRate = 0.5f;
    [SerializeField] float maxSpawnRate = 2.5f;



    // Pre-built Functions
    void OnEnable()
    {
        InvokeRepeating(nameof(SpawnEnemy), spawnDelay, Random.Range(minSpawnRate, maxSpawnRate));
    }
    void OnDisable()
    {
        CancelInvoke(nameof(SpawnEnemy));
    }



    // Custom Functions
    void SpawnEnemy()
    {
        GameObject paper = Instantiate(paperEnemy, transform.position, Quaternion.identity);
        paper.transform.parent = transform;
    }
}
