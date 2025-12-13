using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public RectTransform hpFillRect; // ImageじゃなくRectTransform
    private int maxHP;

    // 最大HPを設定
    public void SetMaxHP(int maxHP)
    {
        this.maxHP = maxHP;
        SetHP(maxHP);
    }

    // HPを更新
    public void SetHP(int currentHP)
    {
        float ratio = (float)currentHP / maxHP;
        ratio = Mathf.Clamp01(ratio);

        hpFillRect.localScale = new Vector3(ratio, 1f, 1f);
    }
}
