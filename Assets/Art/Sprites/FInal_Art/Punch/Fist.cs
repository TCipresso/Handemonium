using UnityEngine;

public class Fist : MonoBehaviour
{
    public static Fist Instance { get; private set; }

    public Animator animator;
    public GameObject meleeHitBox;
    public float fireRate = 1f;
    public float damage = 10f;
    public AudioClip meleeSound;
    public AudioSource audioSource;

    private float nextTimeToFire = 0f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            animator.SetBool("IsPunch", true);
        }
        else
        {
            animator.SetBool("IsPunch", false);
        }
    }

    public void PerformMelee()
    {
        meleeHitBox.SetActive(true);
        PlayMeleeSound();
    }

    public void EndMelee()
    {
        meleeHitBox.SetActive(false);
    }

    void PlayMeleeSound()
    {
        audioSource.PlayOneShot(meleeSound);
    }

    public void DisableBox()
    {
        meleeHitBox.SetActive(false);
    }

}
