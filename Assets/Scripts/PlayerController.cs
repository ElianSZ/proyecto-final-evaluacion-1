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

    public int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI winText;
    public bool gameOver;

    void Start()
    {
        transform.position = initialPos;
        playerAudioSource = GetComponent<AudioSource>();
        UpdateScore(0);
        score = 0;
        gameOverText.gameObject.SetActive(false);
        winText.gameObject.SetActive(false);
    }

    void Update()
    {
        scoreText.text = $"Score: {score}/10";

        // Si la puntuación es igual o mayor a 10, gana el juego
        if (score >= 10)
        {
            Time.timeScale = 0;
            winText.gameObject.SetActive(true);
            gameOver = true;
        }
        
        // Mecanismo de disparo
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            // Se dispara el proyectil
            Instantiate(projectilePrefab, shooter.transform.position, transform.rotation);

            // Ejecuta una vez el audio de disparo
            playerAudioSource.PlayOneShot(shotClip, 1f);
        }

        // Movimiento hacia delante
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        // Movimiento horizontal
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.right * Time.deltaTime * turnSpeed * verticalInput);

        // Movimiento vertical
        verticalInput = Input.GetAxis("Vertical");
        transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);

        // Límites de la escena
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

    // Si colisiona con un collectable, suma 1 punto
    public void OnCollisionEnter(Collision otherCollider)
    {
        if (otherCollider.gameObject.CompareTag("Collectable"))
        {
            Destroy(otherCollider.gameObject);
            score = score + 1;
        }

        // Si colisiona con un obstacle, destruye ambos objetos
        else if (otherCollider.gameObject.CompareTag("Obstacle"))
        {
            Destroy(otherCollider.gameObject);
            Destroy(gameObject);
            gameOverText.gameObject.SetActive(true);
            gameOver = true;
        }
    }

    // Indica que el score se actualiza con los puntos obtenidos
    public void UpdateScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = $"Score: {score}/ 10";
    }
}