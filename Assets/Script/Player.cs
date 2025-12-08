using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100;   // 最大HP
    public int currentHP;     // 現在のHP
    HPBar hpBar;

    void Start()
    {
        // ゲーム開始時、現在HPを最大にする
        currentHP = maxHP;
        // HPバーをシーンから探す
        hpBar = FindObjectOfType<HPBar>();
        hpBar.SetHP(1f); // 満タンで初期化
    }

    // ダメージを受ける関数（外から呼ぶ）
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        // HPバー更新！
        float ratio = (float)currentHP / maxHP;
        hpBar.SetHP(ratio);

        // HPが0以下になったら死ぬ
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    // 回復する関数（必要なら使う）
    public void Heal(int amount)
    {
        currentHP += amount;

        // 最大HP以上にならないようにする
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    // 死亡時の処理
    void Die()
    {
        Debug.Log("Player is Dead!");
        // アニメーション再生、ゲームオーバー画面表示などここに書く
        // Destroy(gameObject); なども可能
        // GameOverManager を探して呼び出す
        GameOverManager gm = FindObjectOfType<GameOverManager>();
        gm.ShowGameOver();

        // 必要ならプレイヤーの操作停止など行う
        Destroy(gameObject);
    }
}
