using UnityEngine;

public class ItemDropper : MonoBehaviour
{
    public static ItemDropper Instance { get; private set; }
    private InputSystem_Actions inputActions;

    [SerializeField] private GameObject[] itemPrefabList;
    private Item currentItem;

    private void Awake()
    {
        Instance = this;
        inputActions = new InputSystem_Actions();

        inputActions.Enable();
        inputActions.Player.Drop.performed += ctx => DropItem();
        inputActions.Player.Rotate.performed += ctx => RotateItem();
    }
    void Start()
    {
        SpawnItem();
        GameManager.Instance.OnContinue += () =>
        {
            StartCoroutine(SpawnNewItemWithAnimation());
        };
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        float moveSpeed = 5f;
        float horizontalInput = inputActions.Player.Move.ReadValue<float>();
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed * Time.deltaTime;
        transform.position += movement;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.5f, 3.5f), transform.position.y, transform.position.z);
    }
    private void SpawnItem()
    {
        int randomIndex = Random.Range(0, itemPrefabList.Length);
        currentItem = Instantiate(itemPrefabList[randomIndex], transform.position, Quaternion.identity, transform).GetComponent<Item>();
    }
    private void DropItem()
    {
        if (currentItem == null) return;
        currentItem.Drop();
        currentItem = null;
        AudioManager.Instance.PlayCraneReleaseItemSound();
    }
    public void SpawnNewItem()
    {
        StartCoroutine(SpawnNewItemWithAnimation());
    }
    private void RotateItem()
    {
        if (currentItem != null)
        {
            StartCoroutine(RotateOverTime(currentItem.transform, 45f, 0.1f));
        }
    }
    private System.Collections.IEnumerator RotateOverTime(Transform target, float angle, float duration)
    {
        Quaternion startRotation = target.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0f, 0f, -angle);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            target.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        target.rotation = endRotation; // Ensure it ends at the exact angle
    }
    private System.Collections.IEnumerator SpawnNewItemWithAnimation()
    {
        AudioManager.Instance.PlayCraneSpawnItemSound();
        Vector3 originalPosition = transform.position;
        Vector3 upPosition = originalPosition + new Vector3(0f, 5f, 0f);
        float animationDuration = 0.5f;
        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            transform.position = Vector3.Lerp(originalPosition, upPosition, elapsed / animationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = upPosition;
        SpawnItem();
        elapsed = 0f;
        while (elapsed < animationDuration)
        {
            transform.position = Vector3.Lerp(upPosition, originalPosition, elapsed / animationDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = originalPosition;
    }
    void OnDestroy()
    {
        inputActions.Disable();
    }
}
