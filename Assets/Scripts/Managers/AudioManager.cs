using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioListener audioListener;

    [Header("Ambient Audio")]
    [SerializeField] private AudioSource theme0AmbientAudioSource;
    [SerializeField] private AudioSource theme1AmbientAudioSource;

    [Header("Other Audio")]
    [SerializeField] private AudioSource gameOverAudioSource;
    [SerializeField] private AudioSource buttonClickAudioSource;
    [SerializeField] private AudioSource doubleMoneyAudioSource;

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
    }
    void Start()
    {
        GameManager.Instance.OnGameOver += HandleGameOver;
        GameManager.Instance.OnContinue += HandleContinue;

        if (GameEnvironment.Instance.GetThemeIndex() == 0)
        {
            theme0AmbientAudioSource.Play();
            theme1AmbientAudioSource.Stop();
        }
        else
        {
            theme1AmbientAudioSource.Play();
            theme0AmbientAudioSource.Stop();
        }
    }
    private void HandleGameOver()
    {
        gameOverAudioSource.Play();
        theme0AmbientAudioSource.Stop();
    }
    private void HandleContinue()
    {
        gameOverAudioSource.Stop();
        theme0AmbientAudioSource.Play();
    }
    public void PlayButtonClickSound()
    {
        PlayRandomPitch(buttonClickAudioSource);
    }
    public void PlayDoubleMoneySound()
    {
        PlayRandomPitch(doubleMoneyAudioSource);
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
