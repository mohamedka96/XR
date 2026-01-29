using UnityEngine;
using TMPro;

public class VitalSignsUIController : MonoBehaviour
{
    public TextMeshProUGUI bpText;
    public TextMeshProUGUI hrText;
    public TextMeshProUGUI rrText;
    public TextMeshProUGUI tempText;
    public TextMeshProUGUI spo2Text;

    public void UpdateUI(PatientCaseData data)
    {
        if (data == null) return;

        // استخدام أسماء المتغيرات الجديدة
        bpText.text = data.bloodPressure;
        hrText.text = data.heartRate.ToString();
        rrText.text = data.respiratoryRate.ToString();
        tempText.text = data.temperature.ToString("F1") + " °C";
        spo2Text.text = data.spO2.ToString() + "%";
    }
}
