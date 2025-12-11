using UnityEngine;
using UnityEngine.UI; // ★重要: 標準の Text コンポーネントを使うため追加

public class WaveManager : MonoBehaviour
{
    // === Inspectorで割り当てる変数 ===
    // Wave数表示用テキスト
    // [SerializeField] private TextMeshProUGUI waveCountText; // TMP版を削除
    [SerializeField] private Text waveCountText;           // ★Text (Legacy) に変更

    // タイマー表示用テキスト
    // [SerializeField] private TextMeshProUGUI waveTimerText; // TMP版を削除
    [SerializeField] private Text waveTimerText;           // ★Text (Legacy) に変更

    // === Inspectorで設定するゲームデータ ===
    [Tooltip("各ウェーブの持続時間（秒）")]
    [SerializeField] private float waveDuration = 60f; // 1ウェーブは60秒

    [Tooltip("クリアに必要な全ウェーブ数")]
    [SerializeField] private int totalWaves = 8; // 全ウェーブ数は「8」に設定

    // === 内部の進行管理変数 ===
    private int currentWave = 1;
    private float waveTimer;
    private bool isGameRunning = true;

    void Start()
    {
        // 初期タイマー設定とUI更新、Wave開始
        waveTimer = waveDuration;
        UpdateWaveUI(); 
        StartWave(currentWave); 
    }

    void Update()
    {
        if (!isGameRunning) return;

        waveTimer -= Time.deltaTime; // リアルタイムで時間を減らす

        // タイマーが0になったときの処理
        if (waveTimer <= 0)
        {
            if (currentWave < totalWaves)
            {
                // 次のWaveへ移行
                currentWave++;
                waveTimer = waveDuration; // タイマーをリセット
                StartWave(currentWave);
            }
            else
            {
                // 全ウェーブ終了
                isGameRunning = false;
                Debug.Log("--- GAME CLEAR! 全ウェーブをクリアしました！ ---");
            }
        }

        // 毎フレームUIを更新
        UpdateWaveUI();
    }

    private void UpdateWaveUI()
    {
        // 1. WAVE数表示 (例: "WAVE 1/8")
        // Text.text に文字列を代入
        waveCountText.text = "WAVE " + currentWave.ToString() + "/" + totalWaves.ToString(); 

        // 2. タイマー表示 (例: "30s")
        // 秒数を切り上げて取得し、"s"を付けて表示
        int seconds = Mathf.CeilToInt(waveTimer); 
        waveTimerText.text = seconds.ToString() + "s";
    }

    private void StartWave(int waveNumber)
    {
        Debug.Log("--- WAVE " + waveNumber + " START! 敵配置を開始します ---");
    }
}