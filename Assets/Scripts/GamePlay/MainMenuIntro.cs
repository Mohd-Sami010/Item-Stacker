using UnityEngine;

public class MainMenuIntro : MonoBehaviour
{
    [SerializeField] private Animator menuUiAnimator;
    [SerializeField] private Animator craneAnimator;
    [SerializeField] private AudioSource menuUiAudioSource;
    [SerializeField] private AudioSource craneAudioSource;

    private void Start()
    {
        menuUiAnimator.SetTrigger("Play");
        craneAnimator.SetTrigger("Play");

        menuUiAudioSource.Play();
        craneAudioSource.Play();
    }
}
