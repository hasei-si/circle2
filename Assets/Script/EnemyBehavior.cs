using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;         // à⁄ìÆë¨ìx
    public float changeDirectionTime = 2f; // ï˚å¸ÇïœÇ¶ÇÈä‘äu

    [Header("Rotation")]
    public float rotateInterval = 2.5f;  // âÒì]Ç∑ÇÈä‘äu
    public float rotateAmount = 90f;     // àÍâÒÇ≈âÒì]Ç∑ÇÈäpìx

    private Vector2 moveDirection;
    private float moveTimer;
    private float rotateTimer;

    // âÊñ ì‡îªíËóp
    private float minX, maxX, minY, maxY;

    void Start()
    {
        SetRandomDirection();
        CalculateScreenBounds();
    }

    void Update()
    {
        Move();
        RotatePeriodically();
    }

    // Å• ìGÇìÆÇ©Ç∑
    void Move()
    {
        moveTimer += Time.deltaTime;

        // àÍíËéûä‘Ç≈ï˚å¸ÇïœÇ¶ÇÈ
        if (moveTimer >= changeDirectionTime)
        {
            SetRandomDirection();
            moveTimer = 0f;
        }

        // é¿ç€ÇÃà⁄ìÆ
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        KeepInsideScreen();
    }

    // Å• ÉâÉìÉ_ÉÄÇ»à⁄ìÆï˚å¸ÇÉZÉbÉg
    void SetRandomDirection()
    {
        moveDirection = Random.insideUnitCircle.normalized;
    }

    // Å• àÍíËéûä‘Ç≤Ç∆Ç…âÒì]
    void RotatePeriodically()
    {
        rotateTimer += Time.deltaTime;

        if (rotateTimer >= rotateInterval)
        {
            rotateTimer = 0f;
            transform.Rotate(new Vector3(0, 0, rotateAmount));
        }
    }

    // Å• âÊñ ÇÃí[Çí¥Ç¶Ç»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
    void KeepInsideScreen()
    {
        Vector3 pos = transform.position;

        if (pos.x < minX || pos.x > maxX)
        {
            moveDirection.x *= -1;
        }
        if (pos.y < minY || pos.y > maxY)
        {
            moveDirection.y *= -1;
        }
    }

    // Å• âÊñ ÇÃå¿äEç¿ïWÇéÊìæ
    void CalculateScreenBounds()
    {
        Camera cam = Camera.main;
        Vector3 leftBottom = cam.ScreenToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));
        Vector3 rightTop = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, cam.nearClipPlane));

        minX = leftBottom.x + 0.5f;
        minY = leftBottom.y + 0.5f;
        maxX = rightTop.x - 0.5f;
        maxY = rightTop.y - 0.5f;
    }
}
