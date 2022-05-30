using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] AudioSource mainMusic;
    [SerializeField] AudioSource secondMusic;

    public void PlayMain()
    {
        secondMusic.Stop();
        mainMusic.Play();
    }

    public void PlaySecondMusic()
    {
        mainMusic.Stop();
        secondMusic.Play();
    }
}
