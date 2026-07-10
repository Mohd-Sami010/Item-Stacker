using UnityEngine;
using TMPro;
using System;

public class Item : MonoBehaviour
{
    private bool isDropped = false;
    private bool checkedForRest = false;
    private bool hitGround = false;
    private float restTimer = 1f;

    [SerializeField] private float horizontalAirResistance = 3f;
    [SerializeField] private GameObject itemStopEffectPrefab;
    [SerializeField] private TextMeshPro textMesh;
    [Header("Theme Colors")]
    [SerializeField] private Color[] theme0ItemTextMeshColors;
    [SerializeField] private Color[] theme1ItemTextMeshColors;
    [SerializeField] private Color[] theme2ItemTextMeshColors;
    [SerializeField] private TMP_FontAsset[] themeTextMeshFonts;

    private Color[] currentThemeColors;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        int colorIndex = UnityEngine.Random.Range(0, theme0ItemTextMeshColors.Length);
        textMesh.color = theme0ItemTextMeshColors[colorIndex];
        // transform.localScale = Vector3.one * Random.Range(0.8f, 1.2f);
        if (GameEnvironment.Instance.GetThemeIndex() == 1)
        {
            currentThemeColors = theme1ItemTextMeshColors;
            textMesh.font = themeTextMeshFonts[GameEnvironment.Instance.GetThemeIndex()];
            int theme1ColorIndex = UnityEngine.Random.Range(0, currentThemeColors.Length);
            textMesh.color = currentThemeColors[theme1ColorIndex];
        }
        else if (GameEnvironment.Instance.GetThemeIndex() == 2)
        {
            currentThemeColors = theme2ItemTextMeshColors;
            textMesh.font = themeTextMeshFonts[GameEnvironment.Instance.GetThemeIndex()];
            int theme1ColorIndex = UnityEngine.Random.Range(0, currentThemeColors.Length);
            textMesh.color = currentThemeColors[theme1ColorIndex];
        }

    }
    void FixedUpdate()
    {
        if (isDropped && rb.bodyType == RigidbodyType2D.Dynamic)
        {
            Vector2 velocity = rb.velocity;
            velocity.x = Mathf.MoveTowards(
                velocity.x,
                0f,
                horizontalAirResistance * Time.deltaTime
            );
            rb.velocity = velocity;
        }

        if (GameManager.Instance.IsGameOver() || hitGround) return;
        if (!isDropped || !checkedForRest) return;

        if (rb.velocity.magnitude < 0.1f && Math.Abs(rb.angularVelocity) < 10f)
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
        rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        rb.interpolation = RigidbodyInterpolation2D.None;
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
        if (hitGround) return;
        checkedForRest = true;

        if (collision.gameObject.CompareTag("Ground"))
        {
            AudioManager.Instance.PlayItemHitSound();
            if (!GameManager.Instance.IsGameOver()) GameManager.Instance.GameOver();
            checkedForRest = false;
            hitGround = true;
        }
    }
    public void Drop()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        // rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        transform.parent = null;
        isDropped = true;
    }
}
