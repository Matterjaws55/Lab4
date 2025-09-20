using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private float horizontalScreenLimit = 10f;
    [SerializeField] private float verticalScreenLimit = 6f;


    private InputSystem_Actions playerInput;

    void Update()
    {
        Move();
    }

    private void OnEnable()
    {
        playerInput = new InputSystem_Actions();
        playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        playerInput.Player.Disable();
    }

    private void Move()
    {
        Vector2 input = playerInput.Player.Move.ReadValue<Vector2>();

        Vector3 direction = new Vector3(input.x, input.y, 0f);
        transform.Translate(direction * Time.deltaTime * speed);

        if (transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, 0);
        }
        if (transform.position.y > verticalScreenLimit || transform.position.y <= -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y, 0);
        }
    }
}