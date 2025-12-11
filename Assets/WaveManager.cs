using UnityEngine;
using UnityEngine.UI; // ★重要: 標準の Text コンポーネントを使うため追加

public class WaveManager : MonoBehaviour
{
    // === Inspectorで割り当てる変数 (UI) ===
    // TextMeshProUGUI から Text (Legacy) に変更します
    [SerializeField] private Text waveCountText;
    [SerializeField] private Text waveTimerText; 

    // === Inspectorで設定するゲームデータ ===
    [Tooltip("各ウェーブの持続時間（秒）")]
    [SerializeField] private float waveDuration = 60f;
    [Tooltip("クリアに必要な全ウェーブ数")]
    [SerializeField] private int totalWaves = 8;
    
    // ★新規追加: リセット対象のオブジェクト
    [Header("Reset Targets")]
    [Tooltip("Wave切り替え時に初期位置に戻したいオブジェクトの配列")]
    [SerializeField] private GameObject[] objectsToReset;

    // === 内部の進行管理変数 ===
    private int currentWave = 1;
    private float waveTimer;
    private bool isGameRunning = true;
    
    // ★新規追加: リセット対象の初期位置を保存する配列
    private Vector3[] initialPositions;

    void Start()
    {
        // 初期タイマー設定
        waveTimer = waveDuration;
        
        // ★新規追加: リセット対象の初期位置を記録
        SaveInitialPositions();
        
        // UI更新とWave開始
        UpdateWaveUI(); 
        StartWave(currentWave); 
    }

    // ★新規追加関数: 初期位置を保存
    private void SaveInitialPositions()
    {
        if (objectsToReset == null) return;
        initialPositions = new Vector3[objectsToReset.Length];
        
        for (int i = 0; i < objectsToReset.Length; i++)
        {
            if (objectsToReset[i] != null)
            {
                initialPositions[i] = objectsToReset[i].transform.position;
            }
        }
    }

    void Update()
    {
        if (!isGameRunning) return;

        waveTimer -= Time.deltaTime; 

        // タイマーが0になったときの処理
        if (waveTimer <= 0)
        {
            if (currentWave < totalWaves)
            {
                // 1. ゲーム状態をリセット
                ResetGameState(); 
                
                // 2. 次のWaveへ移行
                currentWave++;
                waveTimer = waveDuration; // タイマーをリセット
                StartWave(currentWave);
            }
            else
            {
                // 全ウェーブ終了
                isGameRunning = false;
                Debug.Log("--- GAME CLEAR! 全ウェーブをクリアしました！ ---");
                waveTimer = 0; // 念のためタイマーをゼロに固定
            }
        }

        UpdateWaveUI();
    }

    private void UpdateWaveUI()
    {
        // 1. WAVE数表示 (例: "WAVE 1/8")
        // TextMeshProUGUI.text から Text.text に変更
        waveCountText.text = "WAVE " + currentWave.ToString() + "/" + totalWaves.ToString(); 

        // 2. タイマー表示 (例: "30s")
        int seconds = Mathf.CeilToInt(waveTimer); 
        waveTimerText.text = seconds.ToString() + "s";
    }

    // ★新規追加関数: ゲームの状態をリセットする
    private void ResetGameState()
    {
        Debug.Log("--- Game State Reset: Objects moving to initial position ---");
        
        if (objectsToReset != null && initialPositions != null)
        {
            for (int i = 0; i < objectsToReset.Length; i++)
            {
                if (objectsToReset[i] != null && i < initialPositions.Length)
                {
                    // 1. 位置をリセット
                    objectsToReset[i].transform.position = initialPositions[i];
                    
                    // 2. 物理演算の状態をリセット (Rigidbodyがある場合)
                    Rigidbody rb = objectsToReset[i].GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.linearVelocity = Vector3.zero;
                        rb.angularVelocity = Vector3.zero;
                    }
                }
            }
        }
        
        // 敵のスポーンやオブジェクトの破棄など、WaveManager以外で行っているリセット処理もここに追加してください。
    }

    private void StartWave(int waveNumber)
    {
        Debug.Log("--- WAVE " + waveNumber + " START! 敵配置を開始します ---");
        // ここに敵のスポーンロジックなどを記述します。
    }
}