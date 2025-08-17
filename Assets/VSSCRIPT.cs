using System.Collections;
using UnityEngine;
using TMPro;

public class VitalSignsMonitor : MonoBehaviour
{
    [Header("Vitals Texts")]
    public TextMeshProUGUI bpLabel;
    public TextMeshProUGUI prLabel;
    public TextMeshProUGUI rrLabel;
    public TextMeshProUGUI spo2Label;
    public TextMeshProUGUI tempLabel;
    public TextMeshProUGUI hrLabel;

    [Header("Control Buttons")]
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable startButton;
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable stopButton;
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable resetButton;

    private Coroutine monitorCoroutine;
    private bool isMonitoring = false;

    void Start()
    {
        // Connect buttons
        if (startButton != null)
            startButton.selectEntered.AddListener(_ => StartMonitoring());

        if (stopButton != null)
            stopButton.selectEntered.AddListener(_ => StopMonitoring());

        if (resetButton != null)
            resetButton.selectEntered.AddListener(_ => ResetMonitor());

        ResetMonitor();
    }

    void StartMonitoring()
    {
        if (isMonitoring) return;

        isMonitoring = true;
        monitorCoroutine = StartCoroutine(UpdateVitals());
    }

    void StopMonitoring()
    {
        if (!isMonitoring) return;

        isMonitoring = false;
        if (monitorCoroutine != null)
            StopCoroutine(monitorCoroutine);
    }

    void ResetMonitor()
    {
        StopMonitoring();

        SetVitals("--/--", "--", "--", "--", "--", "--");
    }

    IEnumerator UpdateVitals()
    {
        while (true)
        {
            // Simulate data
            int systolic = Random.Range(115, 125);
            int diastolic = Random.Range(75, 85);
            int pr = Random.Range(60, 80);
            int rr = Random.Range(12, 18);
            int spo2 = Random.Range(96, 99);
            float temp = 36.5f + Random.Range(-0.3f, 0.4f);
            int hr = pr;

            // Update values
            SetVitals($"{systolic}/{diastolic}", pr.ToString(), rr.ToString(), spo2.ToString(), temp.ToString("F1"), hr.ToString());

            yield return new WaitForSeconds(1f);
        }
    }

    void SetVitals(string bp, string pr, string rr, string spo2, string temp, string hr)
    {
        // Set text
        bpLabel.text = $"BP: {bp}";
        prLabel.text = $"PR: {pr} bpm";
        rrLabel.text = $"RR: {rr} rpm";
        spo2Label.text = $"SpO2: {spo2}%";
        tempLabel.text = $"Temp: {temp}°C";
        hrLabel.text = hr;

        // Colors (customize freely)
        bpLabel.color = new Color32(255, 165, 0, 255);     // Orange
        prLabel.color = new Color32(255, 255, 0, 255);     // Yellow
        rrLabel.color = new Color32(0, 255, 255, 255);     // Cyan
        spo2Label.color = new Color32(255, 0, 255, 255);   // Magenta
        tempLabel.color = new Color32(255, 255, 255, 255); // White
        hrLabel.color = new Color32(0, 255, 0, 255);       // Green
    }
}