using UnityEngine;
using TMPro;

public class VentilatorUIController : MonoBehaviour
{
    public TextMeshProUGUI rateText;
    public TextMeshProUGUI tidalVolumeText;
    public TextMeshProUGUI peepText;
    public TextMeshProUGUI fio2Text;

    public void UpdateUI(PatientCaseData patientData)
    {
        rateText.text = patientData.ventilatorRate.ToString();
        tidalVolumeText.text = patientData.tidalVolume.ToString();
        peepText.text = patientData.peep.ToString("F1");
        fio2Text.text = (patientData.fio2 * 100).ToString() + "%"; // نعرضها كنسبة مئوية
    }
}
