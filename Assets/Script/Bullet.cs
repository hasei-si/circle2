using UnityEngine;

public class Bullet : MonoBehaviour
{
    // === Inspectorから設定する変数 ===
    public float speed = 10f; // ★PlayerShooter側で上書きされるため、使わなくてOK
    public float lifeTime = 2f; // 自動で消えるまでの時間
    public int damage = 1;
    private Rigidbody2D rb;

    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Start()
    {
        // Rigidbody2Dコンポーネントを取得
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Bullet.cs: Rigidbody2Dがアタッチされていません！");
            return;
        }

        // 💡 以下の行を削除またはコメントアウトします。
        // rb.linearVelocity = transform.up * speed; 

        // lifeTime秒後にこのゲームオブジェクトを削除
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
