using UnityEngine;
using System.Collections;

public class EnemyTurret : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shootInterval = 2.5f;   // ���ˊԊu�i���߁j
    public float bulletSpeed = 3f;

    private Transform player;
    private float timer;

    public int maxHP = 4;
    private int currentHP;

 
    private Vector3 startPos;
   

    private SpriteRenderer sr;
    private bool isDamaging = false;
    private bool isDead = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        timer = shootInterval;
        currentHP = maxHP;
        startPos = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Shoot();
            timer = shootInterval;
        }
    }

    void Shoot()
    {
        Vector2 dir = (player.position - transform.position).normalized;

        GameObject bullet = Instantiate(
            bulletPrefab,
            transform.position,
            Quaternion.identity
        );

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = dir * bulletSpeed;
        }
    }
    IEnumerator DamageEffect(bool isDeath)
    {
        isDamaging = true;
        isDead = isDeath;

        Color originalColor = sr.color;
        Vector3 originalPos = transform.position;

        sr.color = Color.red;

        float shakeTime = 0.2f;
        float shakePower = isDeath ? 0.1f : 0.05f;
        float timer = 0f;

        while (timer < shakeTime)
        {
            transform.position =
                originalPos + (Vector3)Random.insideUnitCircle * shakePower;
            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;

        if (isDeath)
        {
            Destroy(gameObject); // 演出後に消える
        }
        else
        {
            sr.color = originalColor;
            isDamaging = false;
        }
    }

    // ▼ プレイヤー弾との当たり判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
            }

            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDamaging || isDead) return;

        currentHP -= damage;

        if (currentHP <= 0)
        {
            StartCoroutine(DamageEffect(true)); // 死亡演出
        }
        else
        {
            StartCoroutine(DamageEffect(false)); // 通常ダメージ
        }
    }
}
