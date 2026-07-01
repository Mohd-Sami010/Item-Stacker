using UnityEngine;

public class RisingSetup : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject itemDropper;

    private void Start()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        itemDropper.transform.position = new Vector3(0f, 7f, 0f);

    }
}
