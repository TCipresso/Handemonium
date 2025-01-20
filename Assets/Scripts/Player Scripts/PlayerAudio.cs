using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public static PlayerAudio Instance { get; private set; }

    public AudioClip jumpAudioClip;
    public AudioClip walkAudioClip;
    public AudioClip hurtAudioClip;
    private AudioSource audioSource;

    void Awake()
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

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayJumpAudio()
    {
        if (jumpAudioClip != null)
        {
            audioSource.PlayOneShot(jumpAudioClip);
        }
    }

    public void PlayWalkAudio()
    {
        if (walkAudioClip != null && !audioSource.isPlaying)
        {
            audioSource.clip = walkAudioClip;
            audioSource.Play();
        }
    }

    public void StopWalkAudio()
    {
        if (audioSource.clip == walkAudioClip)
        {
            audioSource.Stop();
        }
    }

    public void PlayUserHurtAudio()
    {
        if (hurtAudioClip != null)
        {
            audioSource.PlayOneShot(hurtAudioClip);
        }
    }
}
