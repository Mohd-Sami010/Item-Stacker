using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [Header("Money")]
    [SerializeField] private TextMeshProUGUI moneyEarnedText;
    [SerializeField] private TextMeshProUGUI totalMoneyText;
    [SerializeField] private TextMeshProUGUI doubleMoneyText;

    [Header("Buttons")]
    [SerializeField] private Button adTo2xRewardButton;
    [SerializeField] private Button adToContinueButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [Space(10)]
    [SerializeField] private GameObject loadingUI;

    void Start()
    {
        GameManager.Instance.OnGameOver += ShowGameOverUI;
        GameManager.Instance.OnContinue += () =>
        {
            StartCoroutine(PlayFadeoutAnimation());
        };
        adTo2xRewardButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonClickSound();
            AdUI.Instance.ShowAdLoadingUI();
            PlayCrazyGamesRewardAdToDoubleMoney();
        });
        adToContinueButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonClickSound();
            AdUI.Instance.ShowAdLoadingUI();
            PlayCrazyGamesRewardAdToContinue();
        });
        restartButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonClickSound();
            bool isAdPlayed = adTo2xRewardButton.interactable;
            if (GameManager.Instance.ShouldPlayInterstitialAd() && isAdPlayed)
            {

                AdUI.Instance.ShowAdLoadingUI();
                PlayCrazyGamesMidGameAd();
            }
            else
            {
                loadingUI.SetActive(true);
                GameManager.Instance.RestartGame();
            }
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonClickSound();
            bool isAdPlayed = adTo2xRewardButton.interactable;
            if (GameManager.Instance.ShouldPlayInterstitialAd() && isAdPlayed)
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
    private void PlayCrazyGamesRewardAdToDoubleMoney()
    {
        CrazyAdsController.Instance.ShowRewardedAd((bool isSuccessful) =>
        {
            if (isSuccessful)
            {
                AdUI.Instance.HideAdLoadingUI();
                adToContinueButton.interactable = false;
                adTo2xRewardButton.interactable = false;
                moneyEarnedText.text = $"{ScoreManager.Instance.GetMoneyEarned() * 2}";
                totalMoneyText.text = $"{ScoreManager.Instance.GetMoney() + ScoreManager.Instance.GetMoneyEarned()}";
                ScoreManager.Instance.DoubleMoneyEarned();
                foreach (TextMeshProUGUI text in adTo2xRewardButton.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    text.color = Color.gray;
                }
                AudioManager.Instance.PlayDoubleMoneySound();
            }
            else
            {
                AdUI.Instance.ShowAdFailedUI();
            }
        });
    }
    private void PlayCrazyGamesRewardAdToContinue()
    {
        CrazyAdsController.Instance.ShowRewardedAd((bool isSuccessful) =>
    {
        if (isSuccessful)
        {
            AdUI.Instance.HideAdLoadingUI();
            GameManager.Instance.ContinueGame();
            AudioManager.Instance.PlayButtonClickSound();
        }
        else
        {
            AdUI.Instance.ShowAdFailedUI();
        }
    });
    }
    private void PlayCrazyGamesMidGameAd()
    {
        CrazyAdsController.Instance.ShowMidgameAd(onAdComplete: () =>
                {
                    GameManager.Instance.RestartGame();
                });
    }
    private void ShowGameOverUI()
    {
        CrazyAdsController.Instance.PrefetchRewardedAd();

        scoreText.text = $"{ScoreManager.Instance.GetScore()}";
        highScoreText.text = $"{ScoreManager.Instance.GetHighScore()}";
        moneyEarnedText.text = $"{ScoreManager.Instance.GetMoneyEarned()}";
        totalMoneyText.text = $"{ScoreManager.Instance.GetMoney()}";
        doubleMoneyText.text = $"{ScoreManager.Instance.GetMoneyEarned() * 2}";
        gameObject.SetActive(true);
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
