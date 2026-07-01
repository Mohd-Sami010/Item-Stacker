using UnityEngine;
using CrazyGames;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event System.Action OnGameOver;
    public event System.Action OnContinue;

    private bool isGameOver = false;

    private float playTime = 0f;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        CrazySDK.Init(() =>
        {
            CrazySDK.Game.GameplayStart();
        });
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
        CrazySDK.Game.GameplayStop();
    }
    public void ContinueGame()
    {
        OnContinue?.Invoke();
        CrazySDK.Game.GameplayStart();
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
    public bool IsGameOver()
    {
        return isGameOver;
    }
}
