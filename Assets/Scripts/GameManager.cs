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
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        cameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        StartCoroutine("spawnRandomTarget");
        SpawnCollectable();
    }

    // Update is called once per frame
    void Update()
    {
        InvokeRepeating("RandomSpawnPosition", startDelay, spawnRate);
    }

    public Vector3 RandomSpawnPosition()
    {
        float randomPosX = Random.Range(-lim1, lim1);
        float randomPosY = Random.Range(lim2, lim1);
        float randomPosZ = Random.Range(-lim1, lim1);

        return new Vector3(randomPosX, randomPosY, randomPosZ);
    }

    private IEnumerator spawnRandomTarget()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(spawnRate);

            
            while (targetPositions.Contains(randomSpawnPos))
            {
                randomSpawnPos = RandomSpawnPosition();
            }

            Instantiate(obstaclePrefab, RandomSpawnPosition(), obstaclePrefab.transform.rotation);
            targetPositions.Add(randomSpawnPos);
        }
    }

    public void SpawnCollectable()
    {
        for (float instantiateCollectable = 0; instantiateCollectable < 10; ++instantiateCollectable)
        {
            Instantiate(collectablePrefab, RandomSpawnPosition(), collectablePrefab.transform.rotation);
        }
    }
}