using TMPro;
using UnityEngine;
using System.Collections;

public class AdUI : MonoBehaviour
{
    public static AdUI Instance { get; private set; }

    public enum AdType
    {
        MidGameAd,
        RewardAd,
    }
    [SerializeField] private GameObject adLoadingUI;
    [SerializeField] private GameObject adFailedUI;
    [SerializeField] private TextMeshProUGUI loadingTextMesh;
    [SerializeField] private TextMeshProUGUI adTypeTextMesh;

    private Coroutine loadingAnimationCoroutine;

    private void Awake()
    {
        Instance = this;
        adLoadingUI.SetActive(false);
        adFailedUI.SetActive(false);
    }

    public void ShowAdLoadingUI(AdType adType)
    {
        adLoadingUI.SetActive(true);
        adFailedUI.SetActive(false);

        if (adType == AdType.MidGameAd) adTypeTextMesh.text = "MidGameAd is loading";
        else adTypeTextMesh.text = "RewardAd is loading";

        if (loadingAnimationCoroutine != null)
            StopCoroutine(loadingAnimationCoroutine);

        loadingAnimationCoroutine = StartCoroutine(AnimateLoadingText());
    }

    public void HideAdLoadingUI()
    {
        adLoadingUI.SetActive(false);

        if (loadingAnimationCoroutine != null)
        {
            StopCoroutine(loadingAnimationCoroutine);
            loadingAnimationCoroutine = null;
        }
    }

    public void ShowAdFailedUI()
    {
        HideAdLoadingUI();
        adFailedUI.SetActive(true);
    }

    private IEnumerator AnimateLoadingText()
    {
        string[] texts =
        {
            "Loading Ad",
            "Loading Ad.",
            "Loading Ad..",
            "Loading Ad..."
        };

        int index = 0;

        while (true)
        {
            loadingTextMesh.text = texts[index];
            index = (index + 1) % texts.Length;
            yield return new WaitForSecondsRealtime(0.2f);
        }
    }
}