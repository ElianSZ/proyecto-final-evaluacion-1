using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 130f;
    private float timeDestroy = 3f;

    void Start()
    {
        Destroy(gameObject, timeDestroy);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}