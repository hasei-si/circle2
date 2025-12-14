using UnityEngine;
using UnityEngine.UI; // UIコンポーネント(Text)を使用するために必要

public class WaveManager : MonoBehaviour
{
    // === インスペクターで設定するUIとオブジェクト ===
    
    // Wave数と残り時間を表示するTextコンポーネントをUnityエディタから関連付けます
    public Text waveCountText;
    public Text waveTimerText;
    
    // Wave切り替え時に初期位置に戻したいオブジェクトの配列
    public GameObject[] objectsToReset;

    // === 設定可能なパラメータ ===

    [Header("Wave Settings")]
    [Tooltip("各Waveの制限時間 (秒)")]
    public float timePerWave = 30f; 

    [Tooltip("最終Waveの数")]
    public int maxWaveCount = 8;
    
    // === プライベート変数 (現在の状態を保持) ===

    private int waveCount = 1;      // 現在のWave数 (1からスタート)
    private float waveTimer;        // 現在のWaveの残り時間
    private Vector3[] initialPositions; // リセット対象オブジェクトの初期位置を保存
    
    private bool isGameOver = false; // ゲームオーバー状態を追跡 (今後使用)


    void Start()
    {
        // 初期タイマー設定
        waveTimer = timePerWave; 

        // リセット対象オブジェクトの初期位置を保存
        SaveInitialPositions();
        
        // 初回起動時にUIを更新
        UpdateWaveCountUI();
    }

    void Update()
    {
        if (isGameOver)
        {
            return; // ゲームオーバーなら処理を停止
        }

        // タイマーを減らす
        waveTimer -= Time.deltaTime;
        
        // UIを更新
        UpdateWaveTimerUI();

        // タイマーが0以下になったら次のWaveへ移行
        if (waveTimer <= 0)
        {
            waveTimer = 0; // マイナス表示を防ぐ
            IncreaseWaveCount();
        }
    }

    // --- メソッド ---

    /// <summary>
    /// Wave数を増やし、タイマーとオブジェクトをリセットします。
    /// </summary>
    void IncreaseWaveCount()
    {
        if (waveCount < maxWaveCount)
        {
            // 次のWaveへ
            waveCount++;
            UpdateWaveCountUI();
            
            // タイマーをリセット
            waveTimer = timePerWave; 
            
            // オブジェクトをリセット
            ResetObjectsToInitialPositions();
        }
        else
        {
            // 最大Waveに達したらゲームオーバー処理 (今後実装)
            Debug.Log("ゲームクリア！最大Waveに到達しました。");
            isGameOver = true;
            // TODO: ゲームオーバー画面や勝利処理を呼び出す
        }
    }

    /// <summary>
    /// リセット対象オブジェクトの初期位置を配列に保存します。
    /// </summary>
    void SaveInitialPositions()
    {
        if (objectsToReset.Length > 0)
        {
            initialPositions = new Vector3[objectsToReset.Length];
            for (int i = 0; i < objectsToReset.Length; i++)
            {
                if (objectsToReset[i] != null)
                {
                    initialPositions[i] = objectsToReset[i].transform.position;
                }
            }
        }
    }

    /// <summary>
    /// リセット対象オブジェクトを保存した初期位置に戻します。
    /// </summary>
    void ResetObjectsToInitialPositions()
    {
        if (objectsToReset.Length > 0 && initialPositions.Length == objectsToReset.Length)
        {
            for (int i = 0; i < objectsToReset.Length; i++)
            {
                if (objectsToReset[i] != null)
                {
                    // 位置を初期位置に戻す
                    objectsToReset[i].transform.position = initialPositions[i];
                    
                    // Rigidbodyがあれば速度もリセットする（必要に応じて）
                    Rigidbody rb = objectsToReset[i].GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.linearVelocity = Vector3.zero;
                        rb.angularVelocity = Vector3.zero;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Wave数表示UIを更新します。
    /// </summary>
    void UpdateWaveCountUI()
    {
        if (waveCountText != null)
        {
            waveCountText.text = "Wave " + waveCount.ToString() + "/" + maxWaveCount.ToString();
        }
    }

    /// <summary>
    /// タイマー表示UIを更新します。
    /// </summary>
    // WaveManager.cs の UpdateWaveTimerUI() メソッド内
    void UpdateWaveTimerUI()
    {
        if (waveTimerText != null)
        {
           // 修正後のフォーマット: {0:0.0} 
          // 秒の部分を1桁（0埋めなし）で、小数点以下1桁で表示
           waveTimerText.text = string.Format("残り時間: {0:0.0}", waveTimer); 
     }
    }   
}