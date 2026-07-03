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
            Invoke(nameof(LoadGameScene), 1f);
        });
        shopButton.onClick.AddListener(() => Debug.Log("Shop button clicked!"));
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
