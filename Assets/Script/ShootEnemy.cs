using UnityEngine;
using System.Collections;

public class ShootEnemy : MonoBehaviour
{
    public int maxHP = 2;
    private int currentHP;

    public float moveSpeed = 1f;
    public float moveRange = 1f;
    private Vector3 startPos;
    private float moveTimer = 0f;

    private SpriteRenderer sr;
    private bool isDamaging = false;
    private bool isDead = false;

    void Start()
    {
        currentHP = maxHP;
        startPos = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isDead)
        {
            Move();
        }
    }

    // ▼ 左右移動
    void Move()
    {
        moveTimer += Time.deltaTime;
        float x = Mathf.Sin(moveTimer * moveSpeed) * moveRange;
        transform.position = startPos + new Vector3(x, 0, 0);
    }

    // ▼ ダメージ処理
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

    // ▼ 赤くなって振動する共通演出
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
}
