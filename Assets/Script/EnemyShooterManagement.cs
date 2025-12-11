using UnityEngine;

public class EnemyShooterManagement : MonoBehaviour
{
    public int maxHP = 5;
    private int currentHP;

    public float moveSpeed = 1f;
    public float moveRange = 1f;
    private Vector3 startPos;
    private float moveTimer = 0f;

    void Start()
    {
        currentHP = maxHP;
        startPos = transform.position;
    }

    void Update()
    {
        Move();
    }

    // ▼ 敵が小さく左右に動く（2D用）
    void Move()
    {
        moveTimer += Time.deltaTime;
        float x = Mathf.Sin(moveTimer * moveSpeed) * moveRange;
        transform.position = startPos + new Vector3(x, 0, 0);
    }

    // ▼ 弾からダメージを受ける
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    // ▼ プレイヤーの弾との接触判定（IsTrigger 必須）
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            EnemyBullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
            }

            Destroy(collision.gameObject); // 弾を消す
        }
    }
}