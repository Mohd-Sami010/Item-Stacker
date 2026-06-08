using UnityEngine;
using CrazyGames;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event System.Action OnGameOver;
    public event System.Action OnContinue;

    private bool isGameOver = false;

    private float playTime = 0f;

    [SerializeField] private bool isDesktop = true;

    private void Awake()
    {
        Instance = this;
        if (Application.isMobilePlatform) isDesktop = false;
    }
    private void Start()
    {
        if (isDesktop)
        {
            CrazySDK.Init(() =>
            {
                CrazySDK.Game.GameplayStart();
            });
        }
    }
    private void Update()
    {
        if (isGameOver) return;
        playTime += Time.deltaTime;
    }
    public void GameOver()
    {
        isGameOver = true;
        OnGameOver?.Invoke();
        if (isDesktop) CrazySDK.Game.GameplayStop();
    }
    public void ContinueGame()
    {
        OnContinue?.Invoke();
        if (isDesktop) CrazySDK.Game.GameplayStart();
        isGameOver = false;
    }
    public void RestartGame()
    {
        playTime = 0f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public bool ShouldPlayInterstitialAd()
    {
        return playTime >= 20f;
    }
    public bool IsDesktop()
    {
        return isDesktop;
    }
}
