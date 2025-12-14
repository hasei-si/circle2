using UnityEngine;
using UnityEngine.SceneManagement; // ★ シーン遷移に必須なライブラリを追加

public class SceneLoader : MonoBehaviour
{
    // このメソッドをボタンクリック時に呼び出します
    public void LoadMainScene()
    {
        // "MainScene" は、あなたがゲーム本編のシーンにつけた名前に置き換えてください
        SceneManager.LoadScene("GameScene"); 
    }
}
