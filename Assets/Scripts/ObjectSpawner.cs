using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject healthPackPrefab;

    public float spawnInterval = 2f;

    [Range(0f, 1f)]
    public float healthPackSpawnChance = 0.15f; // 15% health packs, 85% enemies

    public float minDistanceBetweenObjects = 3.5f;
    public float screenPadding = 0.5f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            TrySpawn();
            timer = 0f;
        }
    }

    void TrySpawn()
    {
        Vector2 spawnPos;
        int attempts = 0;

        do
        {
            spawnPos = GetRandomScreenPosition();
            attempts++;

        } while (IsInvalidPosition(spawnPos) && attempts < 20);

        GameObject objectToSpawn;

        // Controlled spawn probability
        if (Random.value < healthPackSpawnChance)
        {
            objectToSpawn = healthPackPrefab;
        }
        else
        {
            objectToSpawn = enemyPrefab;
        }

        Instantiate(objectToSpawn, spawnPos, Quaternion.identity);
    }

    Vector2 GetRandomScreenPosition()
    {
        Camera cam = Camera.main;

        float screenHeight = cam.orthographicSize;
        float screenWidth = screenHeight * cam.aspect;

        float randomX = Random.Range(
            -screenWidth + screenPadding,
             screenWidth - screenPadding
        );

        float randomY = Random.Range(
            -screenHeight + screenPadding,
             screenHeight - screenPadding
        );

        return new Vector2(randomX, randomY);
    }

    bool IsInvalidPosition(Vector2 position)
    {
        Collider2D[] colliders =
            Physics2D.OverlapCircleAll(position, minDistanceBetweenObjects);

        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("Enemy") ||
                col.CompareTag("HealthPack") ||
                col.CompareTag("Wall"))
            {
                return true;
            }
        }

        return false;
    }
}