using UnityEngine;

public class RisingSetup : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject itemDropper;

    private void Start()
    {
        if (GameManager.Instance.IsDesktop())
        {
            SetUpForDesktop();
        }
        else
        {
            SetUpForMobile();
        }
    }
    private void SetUpForMobile()
    {
        Debug.Log("Setting up for mobile");
        transform.position = new Vector3(0f, 1.8f, 0f);
        mainCamera.GetComponent<Camera>().orthographicSize = 10f;
        itemDropper.transform.localPosition = new Vector3(0f, 11.7f, 0f);
    }
    private void SetUpForDesktop()
    {
        Debug.Log("Setting up for desktop");
        transform.position = new Vector3(0f, 0f, 0f);
        mainCamera.GetComponent<Camera>().orthographicSize = 5.5f;
        itemDropper.transform.position = new Vector3(0f, 7f, 0f);
    }
}
