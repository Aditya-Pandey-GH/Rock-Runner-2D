using UnityEngine;

public class CoinSpawner : MonoBehaviour
{

    // Variables
    [SerializeField] GameObject coinEnemy;
    [SerializeField] float spawnDelay = 0f;
    [SerializeField] float minSpawnRate = 9f;
    [SerializeField] float maxSpawnRate = 25f;



    // Pre-built Functions
    void OnEnable()
    {
        InvokeRepeating(nameof(SpawnCoin), spawnDelay, Random.Range(minSpawnRate, maxSpawnRate));
    }
    void OnDisable()
    {
        CancelInvoke(nameof(SpawnCoin));
    }



    // Custom Functions
    void SpawnCoin()
    {
        GameObject coin = Instantiate(coinEnemy, transform.position, Quaternion.identity);
        coin.transform.parent = transform;
    }
}
