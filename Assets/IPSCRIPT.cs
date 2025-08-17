using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VRInfusionPump : MonoBehaviour
{
    public TMP_InputField doseInputField;
    public TMP_InputField rateInputField;
    public TMP_Dropdown fluidDropdown;

    public TextMeshProUGUI doseText;
    public TextMeshProUGUI rateText;
    public TextMeshProUGUI volumeText;
    public TextMeshProUGUI fluidText;
    public TextMeshProUGUI warningText;

    public Button confirmButton;
    public Button startButton;
    public Button stopButton;

    private float rate = 0;
    private float dose = 0;
    private float volume = 0;
    private bool isRunning = false;
    private bool isConfirmed = false;
    private float dosePerMl = 5f;

    private void Start()
    {
        confirmButton.onClick.AddListener(ConfirmSettings);
        startButton.onClick.AddListener(StartInfusion);
        stopButton.onClick.AddListener(StopInfusion);
        warningText.text = "";
    }

    private void Update()
    {
        if (isRunning && isConfirmed && volume > 0)
        {
            float infused = rate * Time.deltaTime / 3600f;
            volume = Mathf.Max(volume - infused, 0);
            dose = volume * dosePerMl;

            if (volume <= 0) StopInfusion();
            UpdateUI();
        }
    }

    private void ConfirmSettings()
    {
        float.TryParse(rateInputField.text, out rate);
        float.TryParse(doseInputField.text, out dose);
        volume = dose / dosePerMl;

        if (dose < 50f || dose > 1000f)
        {
            warningText.text = "Unsafe dose!";
            isConfirmed = false;
            return;
        }

        isConfirmed = true;
        warningText.text = "Confirmed.";
        UpdateUI();
    }

    private void StartInfusion()
    {
        if (!isConfirmed)
        {
            warningText.text = "Confirm first!";
            return;
        }

        isRunning = true;
    }

    private void StopInfusion()
    {
        isRunning = false;
        warningText.text = "Stopped.";
    }

    private void UpdateUI()
    {
        doseText.text = $"{dose:F1}";
        rateText.text = $"{rate:F1}";
        volumeText.text = $"{volume:F1}";
        fluidText.text = fluidDropdown.options[fluidDropdown.value].text;
    }
}