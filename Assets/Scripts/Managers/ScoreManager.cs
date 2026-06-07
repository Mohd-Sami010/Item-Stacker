using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private int score = 0;
    private float highestStackHeight = 0f;

    public event System.Action OnScoreChanged;

    private void Awake()
    {
        Instance = this;
        // PlayerPrefs.SetInt("HighScore", 0);
    }
    public void ItemStacked(float itemHeight)
    {
        if (itemHeight > highestStackHeight)
        {
            highestStackHeight = itemHeight;
            score += 10;
        }
        else
        {
            score += 5;
        }
        if (PlayerPrefs.GetInt("HighScore", 0) < score)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        OnScoreChanged?.Invoke();
    }

    public int GetScore()
    {
        return score;
    }
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }
    public float TowerHeight()
    {
        return highestStackHeight;
    }
}
