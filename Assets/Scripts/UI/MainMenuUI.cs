using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private bool deleteAllPlayerPrefs = false;
    [SerializeField] private Button playButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject loadingUIObject;

    [Header("Socials")]
    [SerializeField] private Button instagramButton;
    [SerializeField] private Button youtubeButton;

    private void Awake()
    {
        if (deleteAllPlayerPrefs) PlayerPrefs.DeleteAll();
        // When playing first time, set the default theme and platform to 0 and 1st shop item to unlocked
        if (!PlayerPrefs.HasKey("SelectedTheme"))
        {
            PlayerPrefs.SetInt("SelectedTheme", 0);
            PlayerPrefs.SetInt("SelectedPlatform", 0);
            PlayerPrefs.SetInt("Item_Theme_0", 1); // Unlock the first theme
            PlayerPrefs.SetInt("Item_Platform_0", 1); // Unlock the first platform
        }
        playButton.onClick.AddListener(() =>
        {
            loadingUIObject.SetActive(true);
            MenuAudioManager.Instance.PlayButton1ClickSound();
            Invoke(nameof(LoadGameScene), 1f);
        });
        shopButton.onClick.AddListener(() =>
        {
            MenuAudioManager.Instance.PlayButton2ClickSound();
            shopUI.SetActive(true);
        });
        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

        string instagramURL = "https://www.instagram.com/mohd_sami501";
        instagramButton.onClick.AddListener(() => Application.OpenURL(instagramURL));

        string youtubeURL = "https://www.youtube.com/@SamiCode_Games";
        youtubeButton.onClick.AddListener(() => Application.OpenURL(youtubeURL));
    }
    void Start()
    {
        MenuAudioManager.Instance.PlayWhoosh1Sound();
    }
    private void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("GameScene");
    }
}
