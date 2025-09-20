using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float cooldownTime = 1f;

    private bool canShoot = true;
    private InputSystem_Actions playerInput;

    private void OnEnable()
    {
        playerInput = new InputSystem_Actions();
        playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        playerInput.Player.Disable();
    }


    void Update()
    {

        float attackPressed = playerInput.Player.Attack.ReadValue<float>();

        if (attackPressed > 0f && canShoot)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(laserPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        canShoot = false;
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }
}