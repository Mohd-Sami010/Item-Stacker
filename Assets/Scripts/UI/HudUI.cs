using UnityEngine;

public class HudUI : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    [SerializeField] private TMPro.TextMeshProUGUI highScoreText;
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private TMPro.TextMeshProUGUI moneyText;
    [SerializeField] private TMPro.TextMeshProUGUI towerHeightText;

    [Space]
    [SerializeField] private GameObject controlsGuidePanel;

    private void Start()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Enable();

        inputActions.Player.ToggleControlsGuide.performed += ctx => ToggleControlsGuide();

        ScoreManager.Instance.OnScoreChanged += UpdateScore;
        UpdateScore();
    }
    private void ToggleControlsGuide()
    {
        controlsGuidePanel.SetActive(!controlsGuidePanel.activeSelf);
    }
    private void UpdateScore()
    {
        moneyText.text = ScoreManager.Instance.GetMoney().ToString();
        scoreText.text = ScoreManager.Instance.GetScore().ToString();
        highScoreText.text = ScoreManager.Instance.GetHighScore().ToString();
        towerHeightText.text = $"{ScoreManager.Instance.GetStackHeight():F1}";
    }
    void OnDestroy()
    {
        ScoreManager.Instance.OnScoreChanged -= UpdateScore;
        inputActions.Player.ToggleControlsGuide.performed -= ctx => ToggleControlsGuide();
        inputActions.Disable();
    }
}
