using UnityEngine;

public class BulletShiho : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 3f;
    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ƒ_ƒ[ƒWˆ—‚Í‚±‚±‚É‘‚­
            Destroy(gameObject);
        }
    }
}