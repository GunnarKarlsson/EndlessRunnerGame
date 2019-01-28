using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject player;
    private readonly float minTime = 2;
    private readonly float maxTime = 4;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        GameObject enemy = Instantiate(enemyPrefab, player.gameObject.transform.position + new Vector3(0.5f, 0f, 30f), player.gameObject.transform.rotation);
        StartCoroutine(SpawnEnemy());
        yield return null;
    }
}
