using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance { get; private set; }
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private int addMmoney;

    [Header("Others")]
    [SerializeField] private AdUI adUI;

    private Animator animator;
    public event System.Action<ShopItem.ItemType, int> OnStartedUsingItem;
    public event System.Action OnItemPurchased;
    void Awake()
    {
        Instance = this;
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") + addMmoney);

        animator = GetComponent<Animator>();
        closeButton.onClick.AddListener(() =>
        {
            MenuAudioManager.Instance.PlayButton2ClickSound();
            StartCoroutine(CloseShop());
        });
        moneyText.text = PlayerPrefs.GetInt("Money", 0).ToString();
        gameObject.SetActive(false);
    }
    private IEnumerator CloseShop()
    {
        animator.SetTrigger("FadeOut");
        MenuAudioManager.Instance.PlayWhoosh2Sound();
        yield return new WaitForSeconds(0.6f);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        MenuAudioManager.Instance.PlayWhoosh2Sound();
        animator.SetTrigger("FadeIn");
    }
    public bool BuyItem(int price)
    {
        int currentMoney = PlayerPrefs.GetInt("Money", 0);
        if (currentMoney >= price)
        {
            MenuAudioManager.Instance.PlayPurchaseSound();
            int oldMoney = currentMoney;
            currentMoney -= price;

            PlayerPrefs.SetInt("Money", currentMoney);

            StopCoroutine(nameof(AnimateMoney));
            StartCoroutine(AnimateMoney(oldMoney, currentMoney, 0.5f));

            OnItemPurchased?.Invoke();
            return true;
        }
        return false;
    }
    private IEnumerator AnimateMoney(int startValue, int endValue, float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.unscaledDeltaTime;

            int value = Mathf.RoundToInt(Mathf.Lerp(startValue, endValue, timer / duration));
            moneyText.text = value.ToString();

            yield return null;
        }

        moneyText.text = endValue.ToString();
    }
    public void StartedUsingItem(ShopItem.ItemType itemType, int itemIndex)
    {
        OnStartedUsingItem?.Invoke(itemType, itemIndex);
    }
}
