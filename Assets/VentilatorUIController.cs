using UnityEngine;
using TMPro;

public class VentilatorUIController : MonoBehaviour
{
    public TextMeshProUGUI modeText;
    public TextMeshProUGUI respRateText;
    public TextMeshProUGUI tidalVolumeText;
    public TextMeshProUGUI fio2Text;
    public TextMeshProUGUI peepText;
    public TextMeshProUGUI peakPressureText;
    public TextMeshProUGUI plateauPressureText;

    public void UpdateUI(PatientCaseData data)
    {
        if (data == null) return;

        // استخدام أسماء المتغيرات الجديدة
        modeText.text = data.ventilationMode;
        respRateText.text = data.ventRespiratoryRate.ToString();
        tidalVolumeText.text = data.tidalVolume.ToString() + " mL";
        fio2Text.text = (data.fio2 * 100).ToString("F0") + "%";
        peepText.text = data.peep.ToString() + " cmH2O";
        peakPressureText.text = data.peakPressure.ToString() + " cmH2O";
        plateauPressureText.text = data.plateauPressure.ToString() + " cmH2O";
    }
}
