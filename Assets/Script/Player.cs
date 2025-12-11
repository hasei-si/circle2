using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHP = 100;   // �ő�HP
    public int currentHP;     // ���݂�HP
    HPBar hpBar;

    void Start()
    {
        // �Q�[���J�n���A����HP���ő�ɂ���
        currentHP = maxHP;
        // HP�o�[���V�[������T��
        hpBar = FindFirstObjectByType<HPBar>();
        hpBar.SetHP(1f); // ���^���ŏ�����
    }

    // �_���[�W���󂯂�֐��i�O����Ăԁj
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        // HP�o�[�X�V�I
        float ratio = (float)currentHP / maxHP;
        hpBar.SetHP(ratio);

        // HP��0�ȉ��ɂȂ����玀��
        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    // �񕜂���֐��i�K�v�Ȃ�g���j
    public void Heal(int amount)
    {
        currentHP += amount;

        // �ő�HP�ȏ�ɂȂ�Ȃ��悤�ɂ���
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    // ���S���̏���
    void Die()
    {
        Debug.Log("Player is Dead!");
        // �A�j���[�V�����Đ��A�Q�[���I�[�o�[��ʕ\���Ȃǂ����ɏ���
        // Destroy(gameObject); �Ȃǂ��\
        // GameOverManager ��T���ČĂяo��
        GameOverManager gm = FindFirstObjectByType<GameOverManager>();
        gm.ShowGameOver();

        // �K�v�Ȃ�v���C���[�̑����~�ȂǍs��
        Destroy(gameObject);
    }
}
