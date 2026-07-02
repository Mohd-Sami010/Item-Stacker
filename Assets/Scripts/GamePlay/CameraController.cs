using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private float deadZone = 3.8f;
    [SerializeField] private Vector3 offset;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        GameManager.Instance.OnContinue += () =>
        {
            SetTarget(null);
        };
    }
    private void LateUpdate()
    {
        if (GameManager.Instance.IsGameOver()) return;

        if (target == null)
        {
            float yPosition = transform.parent.position.y > 0 ? 3.8f : 3.8f;
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0f, yPosition, -10f), smoothSpeed);
            return;
        }
        Vector3 desiredPosition = new Vector3(0, target.position.y, -10f) + offset;
        if (desiredPosition.y < transform.position.y + deadZone && desiredPosition.y > transform.position.y - deadZone) return;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime * 10f);
        transform.position = smoothedPosition;
    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
