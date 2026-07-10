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
        StartCoroutine(LoadingRoutine(2));
    }
    public void LoadMainMenu()
    {
        StartCoroutine(LoadingRoutine(1));
    }
    private System.Collections.IEnumerator LoadingRoutine(int sceneIndex)
    {
        yield return new WaitForSecondsRealtime(1.2f);
        playTime = 0;
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);
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
