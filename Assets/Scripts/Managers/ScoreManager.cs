using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private int score = 0;
    private int money = 0;
    private int moneyAtStart = 0;
    private float highestStackHeight = 0f;

    public event System.Action OnScoreChanged;

    private void Awake()
    {
        Instance = this;
        // PlayerPrefs.SetInt("HighScore", 0);
        money = PlayerPrefs.GetInt("Money", 0);
        moneyAtStart = money;
    }
    public void ItemStacked(float itemHeight)
    {
        if (itemHeight > highestStackHeight)
        {
            highestStackHeight = itemHeight;
            score += 10;
            money += 3;
        }
        else
        {
            score += 5;
            money += 1;
        }
        PlayerPrefs.SetInt("Money", money);
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
    public int GetMoney()
    {
        return money;
    }
    public int GetMoneyEarned()
    {
        return money - moneyAtStart;
    }
    public void DoubleMoneyEarned()
    {
        int moneyEarned = GetMoneyEarned();
        money += moneyEarned;
        PlayerPrefs.SetInt("Money", money);
        OnScoreChanged?.Invoke();
    }
    public float GetStackHeight()
    {
        return highestStackHeight;
    }
}
