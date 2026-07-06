using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ShopUI : MonoBehaviour
{
    public static ShopUI Instance { get; private set; }
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private int money;

    [Header("Others")]
    [SerializeField] private AdUI adUI;

    private Animator animator;
    public event System.Action<ShopItem.ItemType, int> OnStartedUsingItem;
    public event System.Action OnItemPurchased;
    void Awake()
    {
        Instance = this;
        // PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Money", money);

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
            currentMoney -= price;
            PlayerPrefs.SetInt("Money", currentMoney);
            moneyText.text = currentMoney.ToString();
            MenuAudioManager.Instance.PlayPurchaseSound();
            OnItemPurchased?.Invoke();
            return true;
        }
        return false;
    }
    public void StartedUsingItem(ShopItem.ItemType itemType, int itemIndex)
    {
        OnStartedUsingItem?.Invoke(itemType, itemIndex);
    }
}
