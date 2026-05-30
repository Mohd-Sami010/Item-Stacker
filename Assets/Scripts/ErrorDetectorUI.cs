using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ErrorDetectorUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI errorTextMesh;
    [SerializeField] private Button closeButton;
    // [SerializeField] private Button copyButton;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        closeButton.onClick.AddListener(() =>
        {
            Hide();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        });
        // copyButton.onClick.AddListener(() => {
        //     try
        //     {
        //         GUIUtility.systemCopyBuffer = errorTextMesh.text;
        //     }
        //     catch (Exception e)
        //     {
        //         Debug.LogException(new Exception("Failed to Copy to clipboard!\n" + e));
        //     }
        // });
    }
    private void Start()
    {
        Application.logMessageReceived += Application_logMessageReceived;
        Hide();
    }

    private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception)
        {
            Show();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            errorTextMesh.text = "Error: " + condition + "\n" + stackTrace;
        }

    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
