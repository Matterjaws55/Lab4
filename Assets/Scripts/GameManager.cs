using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject meteorPrefab;
    [SerializeField] private GameObject bigMeteorPrefab;

    [Header("Spawn Settings")]
    [SerializeField] private float meteorSpawnInterval = 2f;
    [SerializeField] private float meteorSpawnDelay = 1f;
    [SerializeField] private Vector2 meteorSpawnRange = new Vector2(-8f, 8f);
    [SerializeField] private float spawnY = 7.5f;

    [Header("Settings")]
    [SerializeField] private string sceneToReload = "Week5Lab";
    [SerializeField] private int meteorsBeforeBig = 5;

    public bool gameOver = false;
    public int meteorCount = 0;

    private CinemachineImpulseSource impulseSource;

    private void Start()
    {
        SpawnPlayer();
        InvokeRepeating(nameof(SpawnMeteor), meteorSpawnDelay, meteorSpawnInterval);

        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        if (gameOver)
        {
            CancelInvoke();
            if (Input.GetKeyDown(KeyCode.R))
            {
                ReloadScene();
            }
        }

        if (meteorCount >= meteorsBeforeBig)
        {
            SpawnBigMeteor();
        }
    }

    private void SpawnPlayer()
    {
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
    }

    private void SpawnMeteor()
    {
        Vector3 spawnPos = new Vector3(Random.Range(meteorSpawnRange.x, meteorSpawnRange.y), spawnY, 0);
        GameObject meteor = Instantiate(meteorPrefab, spawnPos, Quaternion.identity);

        meteor.GetComponent<Meteor>().gameManager = this;
    }

    private void SpawnBigMeteor()
    {
        meteorCount = 0;
        Vector3 spawnPos = new Vector3(Random.Range(meteorSpawnRange.x, meteorSpawnRange.y), spawnY, 0);
        Instantiate(bigMeteorPrefab, spawnPos, Quaternion.identity);

        CameraEffects();
    }
    private void CameraEffects()
    {
        CameraShake.instance.ShakeCamera(impulseSource);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(sceneToReload);
    }
}