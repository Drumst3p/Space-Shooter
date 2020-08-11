using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float waitToSpawn;
    private float waitToSpawnPowerUp;
    private Vector3 spawnPos;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private GameObject enemyContainer;

    [SerializeField]
    private GameObject[] _powerupIndex;

    private bool _isPlayerDead = false;
    void Start()
    {
        waitToSpawn = Random.Range(4, 7);
        waitToSpawnPowerUp = Random.Range(10, 15);
    }

    public void StartSpawning()
        {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnPowerUps());
        }

    void Update()
    {
    }

   IEnumerator SpawnEnemies()
        {
        yield return new WaitForSeconds(waitToSpawn);
        while (_isPlayerDead == false)
            {
            spawnPos = new Vector3(Random.Range(-20, 20), 15, 0);
            GameObject newEnemy = Instantiate(enemy, spawnPos, Quaternion.identity);

            // assigning the parent of the newly spawned enemy to the container
            newEnemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(waitToSpawn);
            }
        }

    IEnumerator SpawnPowerUps()
        {
        yield return new WaitForSeconds(waitToSpawn);
        while (_isPlayerDead == false)
            {
            int _randomIndex = Random.Range(0, _powerupIndex.Length);
            spawnPos = new Vector3(Random.Range(-20, 20), 15, 0);

            Instantiate(_powerupIndex[_randomIndex], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(waitToSpawnPowerUp);
            }
        }

    public void PlayerHasDied()
        {
        _isPlayerDead = true;
        }
}
