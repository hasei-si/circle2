using UnityEngine;

public class PlayerSE : MonoBehaviour
{
    public AudioSource seSource;

    public AudioClip shootSE;
    public AudioClip damageSE;
    public AudioClip deathSE;

    public void PlayShoot()
    {
        seSource.PlayOneShot(shootSE);
    }

    public void PlayDamage()
    {
        seSource.PlayOneShot(damageSE);
    }

    public void PlayDeath()
    {
        seSource.PlayOneShot(deathSE);
    }
}
