using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    private bool isDropped = false;
    private bool checkedForRest = false;
    private float restTimer = 1f;
    private float motionDuration = 2f;

    [SerializeField] private TextMeshPro textMesh;
    [SerializeField] private Color[] itemTextMeshColors;
    [SerializeField] private GameObject itemStopEffectPrefab;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        int colorIndex = Random.Range(0, itemTextMeshColors.Length);
        textMesh.color = itemTextMeshColors[colorIndex];

    }
    void Update()
    {
        if (isDropped && checkedForRest && rb.velocity.sqrMagnitude < 1.5f)
        {
            motionDuration -= Time.deltaTime;
            if (motionDuration <= 0f)
            {
                rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, Time.deltaTime * 5f);
            }
            if (motionDuration <= -1f)
            {
                SetBodyToStatic();
            }
        }
        if (isDropped && transform.position.y < -10f)
        {
            GameManager.Instance.GameOver();
            Destroy(gameObject);
            return;
        }
        if (!isDropped || !checkedForRest) return;

        if (rb.velocity.sqrMagnitude < 0.1f)
        {
            restTimer -= Time.deltaTime;
        }
        else
        {
            restTimer = 1f;
        }

        if (restTimer <= 0f)
        {
            SetBodyToStatic();
        }
    }
    private void SetBodyToStatic()
    {
        rb.bodyType = RigidbodyType2D.Static;
        if (ItemDropper.Instance != null) ItemDropper.Instance.SpawnNewItem();
        isDropped = false;
        checkedForRest = false;
        ScoreManager.Instance.ItemStacked(transform.position.y + 0.5f);
        GameObject effect = Instantiate(itemStopEffectPrefab, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        AudioManager.Instance.PlayItemStopSound();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        checkedForRest = true;
    }
    public void Drop()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.parent = null;
        isDropped = true;
    }
}
