using UnityEngine;

public class MenuAudioManager : MonoBehaviour
{
    public static MenuAudioManager Instance { get; private set; }

    [Header("Whooshes")]
    [SerializeField] private AudioSource whoosh1AudioSource;
    [SerializeField] private AudioSource whoosh2AudioSource;
    [Header("UI")]
    [SerializeField] private AudioSource button1ClickAudioSource;
    [SerializeField] private AudioSource button2ClickAudioSource;

    [Header("Transactions")]
    [SerializeField] private AudioSource purchaseAudioSource;

    private void Awake()
    {
        Instance = this;
    }
    # region Whooshes
    public void PlayWhoosh1Sound()
    {
        PlaySound(whoosh1AudioSource);
    }
    public void PlayWhoosh2Sound()
    {
        PlaySound(whoosh2AudioSource);
    }
    #endregion

    #region UI
    public void PlayButton1ClickSound()
    {
        PlaySound(button1ClickAudioSource);
    }
    public void PlayButton2ClickSound()
    {
        PlaySound(button2ClickAudioSource);
    }
    #endregion
    #region Transactions
    public void PlayPurchaseSound()
    {
        PlaySound(purchaseAudioSource);
    }
    #endregion
    private void PlaySound(AudioSource audioSource)
    {
        if (audioSource == null) return;
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }
}
