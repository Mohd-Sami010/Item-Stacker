using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Other Audio")]
    [SerializeField] private AudioSource gameOverAudioSource;
    [SerializeField] private AudioSource ambientAudioSource;
    [SerializeField] private AudioSource buttonClickAudioSource;

    [Header("Crane Audio")]
    [SerializeField] private AudioSource craneSpawnItemAudioSource;
    [SerializeField] private AudioSource craneReleaseItemAudioSource;

    [Header("Item Audio")]
    [SerializeField] private AudioSource floorHitAudioSource;
    [SerializeField] private AudioSource itemHitAudioSource;
    [SerializeField] private AudioSource itemStopAudioSource;

    private void Awake()
    {
        Instance = this;
        // if (Instance == null)
        // {
        //     Instance = this;
        //     DontDestroyOnLoad(gameObject);
        // }
        // else
        // {
        //     Destroy(gameObject);
        //     return;
        // }
    }
    void Start()
    {
        GameManager.Instance.OnGameOver += HandleGameOver;
        GameManager.Instance.OnContinue += HandleContinue;
        ambientAudioSource.Play();
    }
    private void HandleGameOver()
    {
        gameOverAudioSource.Play();
        ambientAudioSource.Stop();
    }
    private void HandleContinue()
    {
        gameOverAudioSource.Stop();
        ambientAudioSource.Play();
    }
    public void PlayButtonClickSound()
    {
        PlayRandomPitch(buttonClickAudioSource);
    }
    public void PlayCraneSpawnItemSound()
    {
        PlayRandomPitch(craneSpawnItemAudioSource);
    }
    public void PlayCraneReleaseItemSound()
    {
        PlayRandomPitch(craneReleaseItemAudioSource);
    }

    public void PlayFloorHitSound()
    {
        PlayRandomPitch(floorHitAudioSource);
    }
    public void PlayItemHitSound()
    {
        PlayRandomPitch(itemHitAudioSource);
    }
    public void PlayItemStopSound()
    {
        PlayRandomPitch(itemStopAudioSource);
    }
    private void PlayRandomPitch(AudioSource audioSource, float minPitch = 0.9f, float maxPitch = 1.1f)
    {
        if (audioSource == null || audioSource.clip == null || !audioSource.isActiveAndEnabled) return;
        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.PlayOneShot(audioSource.clip);
    }
    void OnDestroy()
    {
        GameManager.Instance.OnGameOver -= HandleGameOver;
        GameManager.Instance.OnContinue -= HandleContinue;
    }
}
