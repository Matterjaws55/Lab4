using UnityEngine;

public class BigMeteor : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 0.5f;
    [SerializeField] private float destroyY = -11f;
    [SerializeField] private int health = 5;
    [SerializeField] private GameManager gameManager;

    private int hitCount = 0;

    private void Update()
    {
        Move();
        OutOfBounds();
        CheckHealth();
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

    private void CheckHealth()
    {
        if (hitCount >= health)
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
        }
        else if (collision.CompareTag("Laser"))
        {
            hitCount++;
            Destroy(collision.gameObject);
        }
    }
}