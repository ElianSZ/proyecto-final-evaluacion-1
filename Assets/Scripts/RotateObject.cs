using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float speed = 1200;
    private GameManager gameManagerScript;

    [SerializeField] private int points;

    // float maxLim = 50f;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * speed * Time.deltaTime);

        /*
        if (transform.position.z > maxLim)
        {
            Destroy(gameObject);
        }
        */
    }

    private void OnAddPoints()
    {
        if (!gameManagerScript.gameOver)
        {
            // Dar o quitar puntos
            gameManagerScript.UpdateScore(points);
        }

        if (gameObject.CompareTag("Collectable"))
        {
            Destroy(gameObject);
        }

        else if (gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}