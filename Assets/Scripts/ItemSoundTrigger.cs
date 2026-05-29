using Unity.VisualScripting;
using UnityEngine;

public class ItemSoundTrigger : MonoBehaviour
{
    private bool canPlaySound = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canPlaySound) return;
        if (collision.gameObject.GetComponent<Item>() != null)
        {
            AudioManager.Instance.PlayItemHitSound();
            canPlaySound = false;
            Invoke(nameof(Enable), 0.5f);
        }
        else
        {
            AudioManager.Instance.PlayFloorHitSound();
            canPlaySound = false;
            Invoke(nameof(Enable), 0.5f);
        }
    }
    private void Enable()
    {
        canPlaySound = true;
    }
}
