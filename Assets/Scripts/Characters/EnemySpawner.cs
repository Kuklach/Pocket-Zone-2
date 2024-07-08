using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Characters.AI
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private ushort amountOfSpawns = 2;
        [SerializeField] private float secondsBetweenSpawns = 10;
        [SerializeField] private float radiusForSpawn = 6;

        private ushort spawnedAmount = 0;

        private void Awake()
        {
            StartCoroutine(SpawnTimer());
        }

        private IEnumerator SpawnTimer()
        {
            yield return new WaitForSeconds(secondsBetweenSpawns);
            Instantiate(enemyPrefab, (Vector2)transform.position + Random.insideUnitCircle * radiusForSpawn, Quaternion.identity,
                transform.parent);
            spawnedAmount++;
            if (spawnedAmount < amountOfSpawns)
            {
                StartCoroutine(SpawnTimer());
            }
        }
    }
}