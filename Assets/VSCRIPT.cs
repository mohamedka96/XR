using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class VentilatorSimulatorVR : MonoBehaviour
{
    [Header("المؤشرات الحيوية")]
    public TMP_Text respiratoryRateText; // RR
    public TMP_Text spo2Text;            // SpO2
    public TMP_Text peepText;            // PEEP
    public TMP_Text ieRatioText;         // I:E

    [Header("XR VR Buttons")]
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable toggleUpdateButton;
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable resetButton;

    [Header("الإعدادات")]
    public float updateInterval = 2f;

    // القيم الطبيعية
    private float normalRR = 14f;
    private float normalSpO2 = 97.3f;
    private float normalPEEP = 5f;
    private float normalIERatio = 2f;

    // القيم الحالية
    private float currentRR;
    private float currentSpO2;
    private float currentPEEP;
    private float currentIERatio;

    private bool isAutoUpdate = false;

    void Start()
    {
        ResetToNormalValues();

        if (toggleUpdateButton != null)
            toggleUpdateButton.selectEntered.AddListener(OnToggleUpdatePressed);

        if (resetButton != null)
            resetButton.selectEntered.AddListener(OnResetPressed);
    }

    void OnDestroy()
    {
        if (toggleUpdateButton != null)
            toggleUpdateButton.selectEntered.RemoveListener(OnToggleUpdatePressed);

        if (resetButton != null)
            resetButton.selectEntered.RemoveListener(OnResetPressed);
    }

    void OnToggleUpdatePressed(SelectEnterEventArgs args)
    {
        ToggleAutoUpdate();
    }

    void OnResetPressed(SelectEnterEventArgs args)
    {
        ResetToNormalValues();
    }

    void ToggleAutoUpdate()
    {
        isAutoUpdate = !isAutoUpdate;

        if (isAutoUpdate)
            InvokeRepeating(nameof(UpdateVitals), 0f, updateInterval);
        else
            CancelInvoke(nameof(UpdateVitals));
    }

    void UpdateVitals()
    {
        SimulateNormalBreathing();
        UpdateDisplay();
    }

    void SimulateNormalBreathing()
    {
        currentRR += Random.Range(-0.5f, 0.5f);
        currentSpO2 += Random.Range(-0.1f, 0.1f);
        currentPEEP += Random.Range(-0.2f, 0.2f);
        currentIERatio += Random.Range(-0.05f, 0.05f);

        ClampValues();
    }

    void ClampValues()
    {
        currentRR = Mathf.Clamp(currentRR, normalRR - 4f, normalRR + 4f);
        currentSpO2 = Mathf.Clamp(currentSpO2, normalSpO2 - 2f, normalSpO2 + 1f);
        currentPEEP = Mathf.Clamp(currentPEEP, normalPEEP - 1.5f, normalPEEP + 1.5f);
        currentIERatio = Mathf.Clamp(currentIERatio, normalIERatio - 0.5f, normalIERatio + 0.5f);
    }

    void UpdateDisplay()
    {
        // نص + وحدة
        respiratoryRateText.text = $"{currentRR:F0} bpm";
        spo2Text.text = $"{currentSpO2:F1}%";
        peepText.text = $"{currentPEEP:F1} cmH₂O";
        ieRatioText.text = $"1:{currentIERatio:F2}";

        // تلوين القيم حسب الحالة
        respiratoryRateText.color = (currentRR < 10 || currentRR > 20) ? Color.red : Color.green;
        spo2Text.color = (currentSpO2 < 90f) ? Color.red : (currentSpO2 < 95f ? new Color(1f, 0.65f, 0f) : Color.green); // أحمر - برتقالي - أخضر
        peepText.color = (currentPEEP < 4 || currentPEEP > 6) ? new Color(1f, 0.65f, 0f) : Color.green; // برتقالي أو أخضر
        ieRatioText.color = Color.cyan; // ثابت (أو عدّل حسب الحالة)
    }

    public void ResetToNormalValues()
    {
        currentRR = normalRR;
        currentSpO2 = normalSpO2;
        currentPEEP = normalPEEP;
        currentIERatio = normalIERatio;

        UpdateDisplay();
    }
}