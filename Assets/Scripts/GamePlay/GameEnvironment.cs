using UnityEngine;

public class GameEnvironment : MonoBehaviour
{
    public static GameEnvironment Instance { get; private set; }

    [Range(0, 1)]
    [SerializeField] private int themeIndex = 0;
    [Header("Background")]
    [SerializeField] private SpriteRenderer backgroundRenderer;
    [SerializeField] private Sprite[] backgroundSprites;

    private void Awake()
    {
        Instance = this;
        // int backgroundIndex = PlayerPrefs.GetInt("ThemeIndex", 0);
        int backgroundIndex = themeIndex;
        backgroundRenderer.sprite = backgroundSprites[backgroundIndex];
    }
    public int GetThemeIndex()
    {
        // return PlayerPrefs.GetInt("ThemeIndex", 0);
        return themeIndex;
    }
}
