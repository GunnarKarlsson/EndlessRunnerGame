using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public GameObject armourPowerupPrefab;
    public GameObject magnetPowerupPrefab;
    public GameObject player;
    private readonly float minTime = 5;
    private readonly float maxTime = 10;

    void Start()
    {
        StartCoroutine(SpawnPowerup());
    }

    private IEnumerator SpawnPowerup()
    {
        Vector3 position = player.gameObject.transform.position + new Vector3(0.5f, 0f, 30f);
        position.y = 0f;
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        GameObject enemy = Instantiate(armourPowerupPrefab, position, player.gameObject.transform.rotation);
        StartCoroutine(SpawnPowerup());
    }
}
