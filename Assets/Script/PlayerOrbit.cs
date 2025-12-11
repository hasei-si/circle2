using UnityEngine;

public class PlayerOrbit : MonoBehaviour
{
    // === 1. 変数宣言部分 ===
    
    public Transform center;    // 公転の中心 (インスペクターから設定)
    public float radius = 3f;   // 半径 (インスペクターから設定)
    public float speed = 2f;    // 公転速度 (インスペクターから設定)

    private float angle = 0f;
    
    // 【重要】Rendererコンポーネネトを保持する変数 (透明化対策のため)
    private Renderer myRenderer; 

    void Start()
    {
        // 最初に一度だけRendererコンポーネントを取得
        myRenderer = GetComponent<Renderer>(); 
        
        // Rendererが見つからない場合は警告を出す（デバッグ用）
        if (myRenderer == null)
        {
            Debug.LogWarning("PlayerOrbit: Renderer component not found on Player!");
        }
    }

    void Update()
    {
        // === 2. 既存の移動ロジック ===
        
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
