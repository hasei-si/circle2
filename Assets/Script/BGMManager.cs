using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource audioSource;

    [Header("BGM Clips (3 Waves per clip)")]
    public AudioClip bgmWave1to3;
    public AudioClip bgmWave4to6;
    public AudioClip bgmWave7to9;

    private AudioClip currentClip;

    void Start()
    {
        PlayBGMForWave(1);
    }

    public void PlayBGMForWave(int wave)
    {
        AudioClip nextClip = GetClipByWave(wave);

        // ìØÇ∂BGMÇ»ÇÁêÿÇËë÷Ç¶Ç»Ç¢
        if (currentClip == nextClip || nextClip == null)
            return;

        currentClip = nextClip;
        audioSource.clip = currentClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    AudioClip GetClipByWave(int wave)
    {
        if (wave <= 3)
            return bgmWave1to3;
        else if (wave <= 6)
            return bgmWave4to6;
        else
            return bgmWave7to9;
    }
}
