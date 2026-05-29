using UnityEngine;

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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void ContinueGame()
    {
        OnContinue?.Invoke();
        isGameOver = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
}
