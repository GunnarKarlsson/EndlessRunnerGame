using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public GameObject[] groundPrefabs;
    public Transform playerTransform;
    private static float prefabLength = 10f;
    private float groundY = -2f;
    private float spawnZ = prefabLength / 2;
    private List<GameObject> groundBlocks = new List<GameObject>();
    private int deletionIndex = 0;

    void Start()
    {   
        for (int i = 0; i < groundPrefabs.Length; i++)
        {
            AddNewGround(i);
        }
    }

    void Update()
    {
        if (playerTransform.position.z > (spawnZ - 20))
        {
            AddNewGround(Random.Range(0,3));
            RemoveOldestGround();
        }
    }

    void AddNewGround(int index)
    {
        GameObject go = Instantiate(groundPrefabs[index], new Vector3(0f, groundY, spawnZ), Quaternion.identity) as GameObject;
        go.transform.SetParent(transform);
        spawnZ += prefabLength;
        groundBlocks.Add(go);
    }

    void RemoveOldestGround()
    {
        Destroy(groundBlocks[deletionIndex++]);
    }
}