using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private GameObject loadingUI;

    private bool isPaused = false;

    private void Start()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();

        inputActions.Player.Pause.performed += ctx => TogglePause();
        resumeButton.onClick.AddListener(() =>
        {
            Resume();
        });
        menuButton.onClick.AddListener(() =>
        {
            if (GameManager.Instance.ShouldPlayInterstitialAd())
            {

                AdUI.Instance.ShowAdLoadingUI();
                CrazyAdsController.Instance.ShowMidgameAd(onAdComplete: () =>
                {
                    GameManager.Instance.LoadMainMenu();
                });
            }
            else
            {
                loadingUI.SetActive(true);
                GameManager.Instance.LoadMainMenu();
            }
        });
        gameObject.SetActive(false);
    }
    private void TogglePause()
    {
        if (GameManager.Instance.IsGameOver()) return;
        if (isPaused) Resume();
        else Pause();

    }
    private void Resume()
    {
        GetComponent<Animator>().SetTrigger("FadeOut");
        AudioManager.Instance.PlayButtonClickSound();
        StartCoroutine(ResumeRoutine());
    }
    private System.Collections.IEnumerator ResumeRoutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);

        Time.timeScale = 1;
        gameObject.SetActive(false);
        isPaused = false;
    }
    private void Pause()
    {
        isPaused = true;
        gameObject.SetActive(true);
        GetComponent<Animator>().SetTrigger("FadeIn");
        Time.timeScale = 0;
        AudioManager.Instance.PlayButtonClickSound();
    }
    private void OnDestroy()
    {
        Time.timeScale = 1;
        inputActions.Player.Pause.performed -= ctx => TogglePause();
        inputActions.Disable();
    }
}
