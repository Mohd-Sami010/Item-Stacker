using UnityEngine;

public class AdUI : MonoBehaviour
{
    [SerializeField] private GameObject adLoadingUI;
    [SerializeField] private GameObject adFailedUI;

    private void Awake()
    {
        adLoadingUI.SetActive(false);
        adFailedUI.SetActive(false);
    }
    public void ShowAdLoadingUI()
    {
        adLoadingUI.SetActive(true);
        adFailedUI.SetActive(false);
    }
    public void HideAdLoadingUI()
    {
        adLoadingUI.SetActive(false);
    }
    public void ShowAdFailedUI()
    {
        adLoadingUI.SetActive(false);
        adFailedUI.SetActive(true);
    }
}
