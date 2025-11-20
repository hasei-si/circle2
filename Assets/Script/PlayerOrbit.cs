using UnityEngine;

public class PlayerOrbit : MonoBehaviour
{
    public Transform center;     // ‰ñ‚é’†S
    public float radius = 3f;    // ”¼Œa
    public float speed = 2f;     // ‰ñ“]‘¬“x

    private float angle = 0f;

    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");
        if (input != 0)
        {
            angle += input * speed * Time.deltaTime;
        }

        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        transform.position = center.position + new Vector3(x, y, 0);
    }
}
