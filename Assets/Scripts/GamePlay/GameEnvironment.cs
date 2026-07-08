using UnityEngine;

public class GameEnvironment : MonoBehaviour
{
    public static GameEnvironment Instance { get; private set; }

    [Range(0, 2)]
    [SerializeField] private int themeIndex = 0;

    [Header("Background")]
    [SerializeField] private SpriteRenderer backgroundRenderer;
    [SerializeField] private Sprite[] backgroundSprites;
    [SerializeField] private GameObject[] grounds;

    [Header("Platform")]
    [SerializeField] private GameObject[] platforms;

    private void Awake()
    {
        Instance = this;

        themeIndex = PlayerPrefs.GetInt("TryingTheme", -1) > -1
                    ? PlayerPrefs.GetInt("TryingTheme")
                    : PlayerPrefs.GetInt("SelectedTheme");
        // int backgroundIndex = themeIndex;
        backgroundRenderer.sprite = backgroundSprites[themeIndex];

        for (int i = 0; i < grounds.Length; i++)
        {
            if (i == themeIndex) grounds[i].SetActive(true);
            else grounds[i].SetActive(false);
        }

        // Set platform based on the saved index
        int platformIndex = PlayerPrefs.GetInt("TryingPlatform", -1) > -1
                            ? PlayerPrefs.GetInt("TryingPlatform")
                            : PlayerPrefs.GetInt("SelectedPlatform");

        for (int i = 0; i < platforms.Length; i++)
        {
            if (i == platformIndex) platforms[i].SetActive(true);
            else platforms[i].SetActive(false);
        }
    }
    public int GetThemeIndex()
    {
        return themeIndex;
    }
    private void OnDestroy()
    {
        // Reset the trying theme when the game environment is destroyed
        PlayerPrefs.SetInt("TryingTheme", -1);
        PlayerPrefs.SetInt("TryingPlatform", -1);
    }
}
