using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 2f;
    [SerializeField] private float destroyY = -11f;
    public GameManager gameManager;

    private void Update()
    {
        Move();
        OutOfBounds();
    }

    private void Move()
    {
        transform.Translate(Vector3.down * Time.deltaTime * fallSpeed);
    }

    private void OutOfBounds()
    {
        if (transform.position.y < destroyY)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameManager != null)
                gameManager.gameOver = true;

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Laser"))
        {
            if (gameManager != null)
                gameManager.meteorCount++;

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}