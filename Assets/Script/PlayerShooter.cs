using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    // 💡 インスペクターから設定: 弾のPrefab
    public GameObject bulletPrefab; 
    
    // 💡 インスペクターから設定: 弾の発射速度
    public float bulletSpeed = 15f; 
    
    // 💡 インスペクターから設定: PlayerOrbitスクリプトで使っている「中心」のTransform
    public Transform center;

    void Update()
    {
        // スペースキーが押された瞬間に発射
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireBullet();
        }
    }

    void FireBullet()
    {
        // 1. 弾をプレイヤーの位置に生成 (クローン作成)
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // 2. 弾のRigidbody2Dを取得
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // 3. 発射方向を計算: 「中心の位置」から「現在のプレイヤーの位置」を引くことで、中心へ向かうベクトルが得られます。
            // .normalized でベクトルの長さを1にすることで、純粋な「方向」として使えます。
            Vector2 direction = (center.position - transform.position).normalized;

            // 4. 弾の速度を設定して発射
            rb.linearVelocity = direction * bulletSpeed;
        }
        else
        {
            Debug.LogError("弾のPrefabに Rigidbody2D が見つかりません！");
        }
    }
}
