using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    private GameObject player = null;

    private float spawnTime = 0.0f;
    private float spawnInterval = 0.0f;
    
    [SerializeField] private GameObject[] enemyPrefabs = null;

    [Header("Spawn Setting")]
    [SerializeField, Range(2.0f, 6.0f)] private float minSpawnHeight = 2.0f;
    [SerializeField, Range(2.0f, 6.0f)] private float maxSpawnHeight = 4.5f;
    [SerializeField, Range(0.0f, 10.0f)] private float minSpawnInterval = 1.0f;
    [SerializeField, Range(0.0f, 10.0f)] private float maxSpawnInterval = 3.0f;

    private void Awake() {
        if (minSpawnHeight > maxSpawnHeight) {
            Debug.LogError("minimum spawn height must be lower than maximum spawn height");
            return;
        }

        if (minSpawnInterval > maxSpawnInterval) {
            Debug.LogError("minimum spawn interval must be lower than maximum spawn interval");
            return;
       }
    }

    private void Start() {
        player = GameObject.FindWithTag("Player");
        spawnTime = spawnInterval;
    }

    private void Update() {
        spawnTime += Time.deltaTime;
        if(spawnInterval <= spawnTime) {
            int random = Random.Range(0, enemyPrefabs.Length - 1);
            this.spawnEnemy(enemyPrefabs[random], player.transform.position);
        }
    }

    private void spawnEnemy(GameObject prefab, Vector3 spawnPoint) {
        var enemy = Instantiate(prefab);
        spawnPoint.x += 12.0f; 
        spawnPoint.y = Random.Range(minSpawnHeight, maxSpawnHeight);
        enemy.transform.position = spawnPoint;

        var component = enemy.GetComponent<EnemyControl>();
        if (component == null) enemy.AddComponent<EnemyControl>();
        spawnTime = 0.0f;
        spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }
}
