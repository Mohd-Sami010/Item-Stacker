using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject loadingUIObject;

    [Header("Socials")]
    [SerializeField] private Button instagramButton;
    [SerializeField] private Button youtubeButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            loadingUIObject.SetActive(true);
            playButton.GetComponent<AudioSource>().Play();
            Invoke(nameof(LoadGameScene), 1f);
        });
        shopButton.onClick.AddListener(() =>
        {
            shopButton.GetComponent<AudioSource>().Play();

            // Make shop button change theme and platform type for testing purposes, with 4 modes: 0, 1, 2, 3. 0 = Theme 0, Platform Type 0; 1 = Theme 1, Platform Type 1; 2 = Theme 0, Platform Type 1; 3 = Theme 1, Platform Type 0.
            int currentTheme = PlayerPrefs.GetInt("ThemeIndex", 0);
            int currentPlatform = PlayerPrefs.GetInt("PlatformTypeIndex", 0);

            int newTheme = (currentTheme + 1) % 2;
            int newPlatform = (currentPlatform + 1) % 2;

            PlayerPrefs.SetInt("ThemeIndex", newTheme);
            PlayerPrefs.SetInt("PlatformTypeIndex", newPlatform);
        });
        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

        string instagramURL = "https://www.instagram.com/mohd_sami501";
        instagramButton.onClick.AddListener(() => Application.OpenURL(instagramURL));

        string youtubeURL = "https://www.youtube.com/@SamiCode_Games";
        youtubeButton.onClick.AddListener(() => Application.OpenURL(youtubeURL));
    }
    private void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("GameScene");
    }
}
