using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 2f;
    [SerializeField] private float destroyY = -11f;
    [SerializeField] private GameObject player;
    public GameManager gameManager;

    private float angle;
    private float radius;

    void Start()
    {
        radius = Vector3.Distance(transform.position, player.transform.position);

        Vector3 dir = (transform.position - player.transform.position).normalized;
        angle = Mathf.Atan2(dir.y, dir.x);
    }


    private void Update()
    {
        Move();
        OutOfBounds();
    }

    private void Move()
    {
        float orbitSpeed = fallSpeed * Mathf.Sqrt(radius);


        angle += orbitSpeed * Time.deltaTime;

        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
        transform.position = player.transform.position + offset;

        Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
        float angleToPlayer = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angleToPlayer);
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