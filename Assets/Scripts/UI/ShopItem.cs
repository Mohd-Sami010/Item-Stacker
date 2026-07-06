using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private bool isLocked;
    [SerializeField] private int price;

    public enum ItemType
    {
        Theme,
        Platform
    }
    [Header("Item Properties")]
    [SerializeField] private ItemType itemType;
    [SerializeField] private int itemIndex;

    [Header("Locked UI")]
    [SerializeField] private GameObject lockedUI;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button tryWithAdButton;

    [Header("Unlocked UI")]
    [SerializeField] private GameObject unlockedUI;
    [SerializeField] private Button useButton;

    private void Start()
    {
        ShopUI.Instance.OnStartedUsingItem += HandleStartedUsingItem;
        ShopUI.Instance.OnItemPurchased += HandleItemPurchased;

        priceText.text = price.ToString();

        string itemKey = "Item_" + itemType.ToString() + "_" + itemIndex;
        if (PlayerPrefs.GetInt(itemKey, 0) == 1)
        {
            UnlockedSetUp();
        }
        else
        {
            LockedSetUp();
        }
    }
    private void UnlockedSetUp()
    {
        isLocked = false;
        lockedUI.SetActive(false);

        if (itemType == ItemType.Theme && PlayerPrefs.GetInt("SelectedTheme", 0) == itemIndex)
        {
            useButton.interactable = false;
            useButton.transform.GetChild(0).gameObject.SetActive(false);
            useButton.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (itemType == ItemType.Platform && PlayerPrefs.GetInt("SelectedPlatform", 0) == itemIndex)
        {
            useButton.interactable = false;
            useButton.transform.GetChild(0).gameObject.SetActive(false);
            useButton.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            useButton.interactable = true;
            useButton.transform.GetChild(0).gameObject.SetActive(true);
            useButton.transform.GetChild(1).gameObject.SetActive(false);
        }

        useButton.onClick.AddListener(() =>
        {
            MenuAudioManager.Instance.PlayButton1ClickSound();
            ShopUI.Instance.StartedUsingItem(itemType, itemIndex);
            if (itemType == ItemType.Theme)
            {
                PlayerPrefs.SetInt("SelectedTheme", itemIndex);
            }
            else if (itemType == ItemType.Platform)
            {
                PlayerPrefs.SetInt("SelectedPlatform", itemIndex);
            }
            useButton.interactable = false;
            useButton.transform.GetChild(0).gameObject.SetActive(false);
            useButton.transform.GetChild(1).gameObject.SetActive(true);
        });
    }
    private void LockedSetUp()
    {
        isLocked = true;
        unlockedUI.SetActive(false);
        string itemKey = "Item_" + itemType.ToString() + "_" + itemIndex;
        buyButton.onClick.AddListener(() =>
        {
            if (ShopUI.Instance.BuyItem(price))
            {
                PlayerPrefs.SetInt(itemKey, 1);
                isLocked = false;
                lockedUI.SetActive(false);
                unlockedUI.SetActive(true);
                UnlockedSetUp();
            }
        });
        if (PlayerPrefs.GetInt("Money", 0) < price)
        {
            buyButton.interactable = false;
            buyButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 118, 118, 255);
        }
        else
        {
            buyButton.interactable = true;
            buyButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
        }
    }
    private void HandleStartedUsingItem(ShopItem.ItemType type, int index)
    {
        if (type == itemType && index == itemIndex)
        {
            useButton.interactable = false;
            useButton.transform.GetChild(0).gameObject.SetActive(false);
            useButton.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (type == itemType)
        {
            useButton.interactable = true;
            useButton.transform.GetChild(0).gameObject.SetActive(true);
            useButton.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    private void HandleItemPurchased()
    {
        if (isLocked)
        {
            if (PlayerPrefs.GetInt("Money", 0) < price)
            {
                buyButton.interactable = false;
                buyButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color32(255, 118, 118, 255);
            }
            else
            {
                buyButton.interactable = true;
                buyButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
            }
        }
    }
}
