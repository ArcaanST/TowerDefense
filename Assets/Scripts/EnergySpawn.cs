using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject energyPrefab;

    [SerializeField]
    private float spawnTime = 20f;

    [SerializeField]
    private float spawnReductionPerEnergy = 1f; // reduction in spawn delay per each wolf

    [SerializeField]
    private float minSpawnDelay = 3.5f; // min spawn delay per wolf;

    private float currentSpawnTime;
    private float timer;

    [SerializeField]
    private Transform[] spawnPoints;
    void Start()
    {
        currentSpawnTime = spawnTime;
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timer)
        {
            Spawn();

            currentSpawnTime -= spawnReductionPerEnergy;

            if (currentSpawnTime <= minSpawnDelay)
                currentSpawnTime = minSpawnDelay;

            timer = Time.time + currentSpawnTime;
        }
    }

    void Spawn()
    {
        Instantiate(energyPrefab, spawnPoints[Random.Range(0, spawnPoints.Length)].position,
                Quaternion.identity);
    }
}
