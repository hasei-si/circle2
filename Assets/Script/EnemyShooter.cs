using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootInterval = 1.5f;

    private Transform player;
    private float timer = 0f;

    void Start()
    {
        // タグから Player を探す
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.Log("Player が見つかりません（Tag を確認してください）");
        }
    }

    void Update()
    {
        if (player == null) return;

        timer += Time.deltaTime;

        if (timer >= shootInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Vector2 dir = (player.position - firePoint.position).normalized;
        bullet.GetComponent<EnemyBullet>().SetDirection(dir);
    }
}
