using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float destroyY = 11f;

    private void Update()
    {
        Move();
        OutOfBounds();
    }

    private void Move()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    private void OutOfBounds()
    {
        if (transform.position.y > destroyY)
        {
            Destroy(gameObject);
        }
    }
}