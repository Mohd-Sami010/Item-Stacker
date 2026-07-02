using UnityEngine;

public class RisingSetup : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject itemDropper;
    [SerializeField] private float offsetY = -1f;

    private void Update()
    {
        if (GameManager.Instance.IsGameOver()) return;

        float towerHeight = ScoreManager.Instance.TowerHeight();

        if (towerHeight > 5f)
        {
            float smoothSpeed = 0.125f;
            transform.position = Vector3.Lerp(transform.position, new Vector3(0f, towerHeight + offsetY, 0f), smoothSpeed * Time.deltaTime * 60f);
        }
    }
}
