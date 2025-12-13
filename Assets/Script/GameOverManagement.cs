using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    // ゲームオーバー画面（Panel）を指定
    public GameObject gameOverPanel;
    public GameObject gameOverText;

    void Start()
    {
        // 最初はゲームオーバー画面を非表示にする
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    // ここを呼べばゲームオーバー画面が表示される
    public void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            gameOverText.SetActive(true);

            // 時間止めたい場合はこれ追加
            // Time.timeScale = 0f;
        }
    }
}
