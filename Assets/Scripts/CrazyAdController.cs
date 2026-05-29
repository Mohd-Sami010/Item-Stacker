using UnityEngine;
using System;
using CrazyGames;

public class CrazyAdsController : MonoBehaviour
{
    public static CrazyAdsController Instance;

    [HideInInspector]
    public bool isAdblockPresent = false;

    private void Awake()
    {
        // Setup Singleton lifecycle
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // Fallback initialization check if a scene is launched straight from the editor
        if (CrazySDK.IsAvailable && !CrazySDK.IsInitialized)
        {
            CrazySDK.Init(() => { CheckAdblockStatus(); });
        }
        else
        {
            CheckAdblockStatus();
        }
    }

    /// <summary>
    /// Checks if the user is running an adblocker and stores it locally.
    /// </summary>
    private void CheckAdblockStatus()
    {
        if (!CrazySDK.IsAvailable) return;

        CrazySDK.Ad.HasAdblock((adblockPresent) =>
        {
            isAdblockPresent = adblockPresent;
            Debug.Log($"[CrazyAds] Adblocker detection status: {adblockPresent}");
        });
    }

    /// <summary>
    /// Prefetches a rewarded ad in the background to eliminate loading delay.
    /// Call this a few moments BEFORE displaying a rewarded button.
    /// </summary>
    public void PrefetchRewardedAd()
    {
        if (CrazySDK.IsAvailable && CrazySDK.IsInitialized)
        {
            CrazySDK.Ad.PrefetchAd(CrazyAdType.Rewarded);
            Debug.Log("[CrazyAds] Prefetching a rewarded ad slot...");
        }
    }

    /// <summary>
    /// Displays a standard Midgame Interstitial ad break.
    /// </summary>
    public void ShowMidgameAd(Action onAdComplete = null)
    {
        if (!CrazySDK.IsAvailable || !CrazySDK.IsInitialized)
        {
            onAdComplete?.Invoke();
            return;
        }

        CrazySDK.Ad.RequestAd(
            CrazyAdType.Midgame,
            () => Debug.Log("[CrazyAds] Midgame ad started."),
            (error) =>
            {
                Debug.LogWarning($"[CrazyAds] Midgame ad error: {error}");
                onAdComplete?.Invoke(); // still proceed even on error
            },
            () =>
            {
                Debug.Log("[CrazyAds] Midgame ad finished.");
                onAdComplete?.Invoke();
            }
        );
    }

    /// <summary>
    /// Requests a Rewarded ad and triggers a callback returning true (success) or false (failed/blocked).
    /// </summary>
    public void ShowRewardedAd(Action<bool> onAdResult)
    {
        // Editor / Non-WebGL Environment fallbacks
        if (!CrazySDK.IsAvailable || !CrazySDK.IsInitialized)
        {
            Debug.Log("[CrazyAds] SDK not active. Granting editor placeholder reward.");
            onAdResult?.Invoke(true);
            return;
        }

        CrazySDK.Ad.RequestAd(
            CrazyAdType.Rewarded,
            () =>
            {
                Debug.Log("Rewarded ad started playback.");
            },
            (error) =>
            {
                Debug.LogWarning($"Rewarded ad failed to render: {error}");
                onAdResult?.Invoke(false); // Return False
            },
            () =>
            {
                Debug.Log("Rewarded ad completed cleanly.");
                onAdResult?.Invoke(true);  // Return True
            }
        );
    }
}