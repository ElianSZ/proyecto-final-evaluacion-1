using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Vector3 initialPos = new Vector3(0, 100, 0);
    private float horizontalInput;
    private float verticalInput;
    public float speed = 50f;
    public float turnSpeed = 20f;
    float yLim = 200f;
    float xLim = 200f;
    float zLim = 200f;
    float sLim = 0f;

    public GameObject projectilePrefab;
    public GameObject shooter;
    private AudioSource playerAudioSource;
    public AudioClip shotClip;

    public bool gameOver;
    public int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI winText;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = initialPos;
        playerAudioSource = GetComponent<AudioSource>();
        UpdateScore(0);
        score = 0;
        gameOverText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = $"Score: {score}";

        if (score >= 10)
        {
            winText.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            // Se dispara el proyectil
            Instantiate(projectilePrefab, shooter.transform.position, transform.rotation);

            playerAudioSource.PlayOneShot(shotClip, 1f);                                            // Ejecuta una vez el audio de disparo
        }

        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        // Movimiento horizontal
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.right * Time.deltaTime * turnSpeed * verticalInput);

        // Movimiento vertical
        verticalInput = Input.GetAxis("Vertical");
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);

        if (transform.position.x > xLim)
        {
            transform.position = new Vector3(xLim, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -xLim)
        {
            transform.position = new Vector3(-xLim, transform.position.y, transform.position.z);
        }

        if (transform.position.y > yLim)
        {
            transform.position = new Vector3(transform.position.x, yLim, transform.position.z);
        }

        if (transform.position.y < -sLim)
        {
            transform.position = new Vector3(transform.position.x, -sLim, transform.position.z);
        }

        if (transform.position.z > zLim)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zLim);
        }

        if (transform.position.z < -zLim)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zLim);
        }
    }

    public void OnCollisionEnter(Collision otherCollider)
    {
        if (!gameOver)
        {
            if (otherCollider.gameObject.CompareTag("Collectable"))
            {
                Destroy(otherCollider.gameObject);
                score = score + 1;
            }

            else if (otherCollider.gameObject.CompareTag("Obstacle"))
            {
                gameOverText.gameObject.SetActive(true);
                Destroy(gameObject);
            }
        }
    }

    public void UpdateScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = $"Score: {score}";
    }
}