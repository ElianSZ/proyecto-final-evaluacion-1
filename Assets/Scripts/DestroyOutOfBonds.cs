using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBonds : MonoBehaviour
{
    private float upperLim = 200f;
    private float lowerLim = 0f;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Bala supera límite superior de pantalla
        if (transform.position.z > upperLim)
        {
            Destroy(gameObject);
        }

        // Animal supera límite inferior de pantalla
        if (transform.position.z < lowerLim)
        {
            player = GameObject.Find("Player");
            Destroy(gameObject);
            Debug.Log("GAME OVER");
        }
    }
}