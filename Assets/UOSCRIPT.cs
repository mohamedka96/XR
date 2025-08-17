using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class KidneyMonitorUI_VR : MonoBehaviour
{
    [Header("Urine Output Display")]
    public TMP_Text outputPerHourText;
    public TMP_Text totalOutputText;
    public TMP_Text colorText;
    public TMP_Text gravityText;
    public TMP_Text pHText;

    [Header("Control Buttons (XR Interactables)")]
    public XRBaseInteractable startButton;
    public XRBaseInteractable stopButton;
    public XRBaseInteractable resetButton;

    private bool isMonitoring = false;
    private float simulatedTotal = 0f;

    void Start()
    {
        if (startButton != null)
            startButton.selectEntered.AddListener(_ => StartMonitoring());

        if (stopButton != null)
            stopButton.selectEntered.AddListener(_ => StopMonitoring());

        if (resetButton != null)
            resetButton.selectEntered.AddListener(_ => ResetMonitor());
    }

    void StartMonitoring()
    {
        if (isMonitoring) return;

        isMonitoring = true;
        StartCoroutine(UpdateSimulation());
    }

    void StopMonitoring()
    {
        isMonitoring = false;
        StopAllCoroutines();
    }

    void ResetMonitor()
    {
        isMonitoring = false;
        StopAllCoroutines();
        simulatedTotal = 0f;
        UpdateUI(0f, 0f, "Clear", "1.010", "6.5");
    }

    IEnumerator UpdateSimulation()
    {
        while (isMonitoring)
        {
            SimulateData();
            yield return new WaitForSeconds(1f);
        }
    }

    void SimulateData()
    {
        float hourlyOutput = Random.Range(30f, 80f);
        simulatedTotal += hourlyOutput / 60f;

        string color = GetRandomColor();
        string gravity = Random.Range(1.010f, 1.030f).ToString("F3");
        string ph = Random.Range(4.5f, 8f).ToString("F1");

        UpdateUI(hourlyOutput, simulatedTotal, color, gravity, ph);
    }

    void UpdateUI(float hourOut, float total, string color, string gravity, string ph)
    {
        if (outputPerHourText != null) outputPerHourText.text = $"{hourOut:F0} ml/h";
        if (totalOutputText != null) totalOutputText.text = $"{total:F0} ml / 24h";
        if (colorText != null) colorText.text = color;
        if (gravityText != null) gravityText.text = gravity;
        if (pHText != null) pHText.text = ph;
    }

    string GetRandomColor()
    {
        string[] colors = { "Clear", "Amber", "Dark Yellow", "Red-Tint" };
        return colors[Random.Range(0, colors.Length)];
    }
}