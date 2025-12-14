using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager Instance;

    public AudioSource bgmSource;

    [Header("Wave BGM")]
    public AudioClip bgmWave1to3;
    public AudioClip bgmWave4to6;
    public AudioClip bgmWave7to9;

    private AudioClip currentClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        PlayBGMByWave(1); // 仮スタート
    }

    public void PlayBGMByWave(int wave)
    {
        AudioClip nextClip = null;

        if (wave >= 1 && wave <= 3)
            nextClip = bgmWave1to3;
        else if (wave >= 4 && wave <= 6)
            nextClip = bgmWave4to6;
        else if (wave >= 7 && wave <= 9)
            nextClip = bgmWave7to9;

        // 同じBGMなら何もしない
        if (nextClip == null || nextClip == currentClip)
            return;

        currentClip = nextClip;
        bgmSource.clip = nextClip;
        bgmSource.loop = true;
        bgmSource.Play();
    }
}
