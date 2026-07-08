using UnityEngine;
using UnityEngine.UI;

public class TutorialGuideUI : MonoBehaviour
{
    [SerializeField] private Button closeButton;

    private void Start()
    {
        if (ScoreManager.Instance.GetHighScore() > 0) Destroy(gameObject);
        closeButton.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayButtonClickSound();
            gameObject.SetActive(false);
        });
    }
}
