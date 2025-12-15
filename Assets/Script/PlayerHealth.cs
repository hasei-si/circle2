using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 5;
    private int currentHP;
    public HPBar hpBar;
    public GameOverManager gameOverManager;
    public PlayerSE playerSE;


    private SpriteRenderer sr;
    private bool isDamaging = false;

    void Start()
    {
        currentHP = maxHP;
        sr = GetComponent<SpriteRenderer>();
        playerSE = GetComponent<PlayerSE>();


        if (hpBar != null)
        {
            hpBar.SetMaxHP(maxHP);
        }
    }


public void TakeDamage(int damage)
    {
        if (isDamaging) return;

        currentHP -= damage;
        if (playerSE != null)
        {
            playerSE.PlayDamage();
        }



        Debug.Log("Player HP: " + currentHP);
        currentHP = Mathf.Max(currentHP, 0);

        if (hpBar != null)
        {
            hpBar.SetHP(currentHP);
        }

        if (currentHP <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(DamageEffect());
        }
    }

    IEnumerator DamageEffect()
    {
        isDamaging = true;

        Color originalColor = sr.color;
        Vector3 originalPos = transform.position;

        sr.color = Color.red;

        float shakeTime = 0.2f;
        float shakePower = 0.07f;
        float timer = 0f;

        while (timer < shakeTime)
        {
            transform.position =
                originalPos + (Vector3)Random.insideUnitCircle * shakePower;
            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
        sr.color = originalColor;

        isDamaging = false;
    }

    void Die()
    {
        Debug.Log("Player Dead");
        gameObject.SetActive(false);
        if (playerSE != null)
        {
            playerSE.PlayDeath();
        }


        if (gameOverManager != null)
        {
            gameOverManager.ShowGameOver();
        }

    }
}
