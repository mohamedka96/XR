using UnityEngine;
using TMPro;

public class InfusionPumpUIController : MonoBehaviour
{
    public TextMeshProUGUI drugNameText;
    public TextMeshProUGUI rateText;
    public TextMeshProUGUI vtbiText;

    public void UpdateUI(PatientCaseData patientData)
    {
        drugNameText.text = patientData.infusionDrugName;
        rateText.text = patientData.infusionRate.ToString("F1") + " ml/hr";
        vtbiText.text = patientData.infusionVTBI.ToString() + " ml";
    }
}
