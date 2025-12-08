using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Image fillImage; // HPバーの中身（Image）

    public void SetHP(float ratio)
    {
        // ratio は 0〜1 で渡す（0 = 空 / 1 = 満タン）
        fillImage.fillAmount = ratio;
    }
}
