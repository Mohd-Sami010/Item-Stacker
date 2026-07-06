using UnityEngine;

public class GameEnvironment : MonoBehaviour
{
    public static GameEnvironment Instance { get; private set; }

    [Range(0, 1)]
    [SerializeField] private int themeIndex = 0;

    [Header("Background")]
    [SerializeField] private SpriteRenderer backgroundRenderer;
    [SerializeField] private Sprite[] backgroundSprites;

    [Header("Platform")]
    [SerializeField] private GameObject type0Platform;
    [SerializeField] private GameObject type1Platform;

    [Header("Ground")]
    [SerializeField] private GameObject theme0Ground;
    [SerializeField] private GameObject theme1Ground;

    private void Awake()
    {
        Instance = this;
        themeIndex = PlayerPrefs.GetInt("SelectedTheme", 0);
        // int backgroundIndex = themeIndex;
        backgroundRenderer.sprite = backgroundSprites[themeIndex];

        theme0Ground.SetActive(themeIndex == 0);
        theme1Ground.SetActive(themeIndex == 1);

        // Set platform based on the saved index
        int platformIndex = PlayerPrefs.GetInt("SelectedPlatform", 0);
        type0Platform.SetActive(platformIndex == 0);
        type1Platform.SetActive(platformIndex == 1);
    }
    public int GetThemeIndex()
    {
        // return PlayerPrefs.GetInt("ThemeIndex", 0);
        return themeIndex;
    }
}
