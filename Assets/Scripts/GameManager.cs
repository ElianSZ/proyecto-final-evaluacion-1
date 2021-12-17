using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public List<Vector3> targetPositions;
    public GameObject collectablePrefab;
    public Vector3 randomSpawnPos;

    private AudioSource cameraAudioSource;

    private float lim1 = 200f;
    private float lim2 = 0f;
    private float spawnRate = 5f;
    private float startDelay = 5f;

    private string message;
    public bool gameOver;

    void Start()
    {
        cameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        StartCoroutine("spawnRandomTarget");
        SpawnCollectable();
    }

    void Update()
    {
        // Invica repetidamente la función RandomSpawnPosition
        InvokeRepeating("RandomSpawnPosition", startDelay, spawnRate);
    }

    // Posición de spawnear de los obstáculos
    public Vector3 RandomSpawnPosition()
    {
        float randomPosX = Random.Range(-lim1, lim1);
        float randomPosY = Random.Range(lim2, lim1);
        float randomPosZ = Random.Range(-lim1, lim1);

        return new Vector3(randomPosX, randomPosY, randomPosZ);
    }

    // Controlador de spawnear los obstáculos
    private IEnumerator spawnRandomTarget()
    {
        while (!gameOver)
        {
            // Cada 5s se spawnea un obstáculo
            yield return new WaitForSeconds(spawnRate);

            
            while (targetPositions.Contains(randomSpawnPos))
            {
                randomSpawnPos = RandomSpawnPosition();
            }

            // Instancia los obstáculos
            Instantiate(obstaclePrefab, RandomSpawnPosition(), obstaclePrefab.transform.rotation);
            targetPositions.Add(randomSpawnPos);
        }
    }

    // Spawneo de los colectables
    public void SpawnCollectable()
    {
        for (float instantiateCollectable = 0; instantiateCollectable < 10; ++instantiateCollectable)
        {
            Instantiate(collectablePrefab, RandomSpawnPosition(), collectablePrefab.transform.rotation);
        }
    }
}