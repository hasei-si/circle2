using UnityEngine;

public class EnemyShooter2D : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public Transform player;
    public float shootInterval = 1.5f;

    private float timer = 0f;

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
        // 弾を生成
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // プレイヤー方向ベクトル計算
        Vector2 dir = (player.position - firePoint.position);

        // 弾に方向を渡す
        bullet.GetComponent<BulletShiho>().SetDirection(dir);
    }
}