using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool gameOver;
    public GameObject obstaclePrefab;
    public List<Vector3> obstaclePositions;
    public Vector3 randomSpawnPos;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;

    private float lim1 = 200f;
    private float lim2 = 0f;
    private float spawnRate = 1f;
    private float startDelay = 1f;

    private int score;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("spawnRandomTarget");
        score = 0;
        UpdateScore(0);
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

            while (obstaclePositions.Contains(randomSpawnPos))
            {
                randomSpawnPos = RandomSpawnPosition();
            }

            Instantiate(obstaclePrefab, randomSpawnPos, obstaclePrefab.transform.rotation);
            obstaclePositions.Add(randomSpawnPos);
        }
    }

    public void UpdateScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = $"Score: {score}";
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverText.gameObject.SetActive(true);
    }
}




/*
public Vector3 RandomSpawnPosition()
{
    float randomIntX = Random.Range(-lim1, lim1);
    float randomIntY = Random.Range(lim2, lim1);
    float randomIntZ = Random.Range(-lim1, lim1);
    float randomPosX = randomIntX;
    float randomPosY = randomIntY;
    float randomPosZ = randomIntZ;

    return new Vector3(randomPosX, randomPosY, randomPosZ);
}

private IEnumerator spawnRandomTarget()
{
    while (!gameOver)
    {
        yield return new WaitForSeconds(spawnRate);

        Instantiate(obstaclePrefab, randomSpawnPos, obstaclePrefab.transform.rotation);

        while (obstaclePositions.Contains(randomSpawnPos))
        {
            randomSpawnPos = RandomSpawnPosition();
        }
    }
}
*/



/*
    public void RandomSpawnPosition()
    {
        if (!gameOver)
        {
            float randomPosX = Random.Range(-lim1, lim1);
            float randomPosY = Random.Range(lim2, lim1);
            float randomPosZ = Random.Range(-lim1, lim1);

            spawnPosition = new Vector3(randomPosX, randomPosY, randomPosZ);

            Instantiate(obstaclePrefab, spawnPosition, obstaclePrefab.transform.rotation);
        }
    }
    */