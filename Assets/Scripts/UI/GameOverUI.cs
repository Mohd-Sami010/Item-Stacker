using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private Button adToContinueButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject adFailedPanel;

    private bool continuedOnce = false;

    void Start()
    {
        GameManager.Instance.OnGameOver += ShowGameOverUI;
        GameManager.Instance.OnContinue += () =>
        {

            StartCoroutine(PlayFadeoutAnimation());
        };

        adToContinueButton.onClick.AddListener(() =>
        {
            CrazyAdsController.Instance.ShowRewardedAd((bool isSuccessful) =>
    {
        if (isSuccessful)
        {
            GameManager.Instance.ContinueGame();
            AudioManager.Instance.PlayButtonClickSound();
            continuedOnce = true;
        }
        else
        {
            adFailedPanel.SetActive(true);
        }
    });
        });
        restartButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonClickSound();
            if (GameManager.Instance.ShouldPlayInterstitialAd())
            {
                CrazyAdsController.Instance.ShowMidgameAd(onAdComplete: () =>
                {
                    GameManager.Instance.RestartGame();
                });
            }
            else
            {
                GameManager.Instance.RestartGame();
            }
        });

        gameObject.SetActive(false);
    }
    private void ShowGameOverUI()
    {
        CrazyAdsController.Instance.PrefetchRewardedAd();

        scoreText.text = $"{ScoreManager.Instance.GetScore()}";
        highScoreText.text = $"{ScoreManager.Instance.GetHighScore()}";
        gameObject.SetActive(true);

        if (continuedOnce)
        {
            adToContinueButton.interactable = false;
        }
    }
    void OnDestroy()
    {
        GameManager.Instance.OnGameOver -= ShowGameOverUI;
    }
    private System.Collections.IEnumerator PlayFadeoutAnimation()
    {
        GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
