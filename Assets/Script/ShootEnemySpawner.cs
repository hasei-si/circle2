using UnityEngine;

public class ShootEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    [Header("出現間隔")]
    public float spawnInterval = 2.0f;

    [Header("画面端からの余白")]
    public float margin = 0.5f;

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPos = GetRandomPositionInCamera();
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    // ▼ 画面内ランダム座標取得
    Vector2 GetRandomPositionInCamera()
    {
        Camera cam = Camera.main;

        float height = cam.orthographicSize;
        float width = height * cam.aspect;

        float x = Random.Range(-width + margin, width - margin);
        float y = Random.Range(-height + margin, height - margin);

        return new Vector2(x, y);
    }
}
