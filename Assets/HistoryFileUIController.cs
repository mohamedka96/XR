using UnityEngine;
using TMPro;

public class HistoryFileUIController : MonoBehaviour
{
    public TextMeshProUGUI patientInfoText; // سيعرض الاسم، العمر، إلخ.
    public TextMeshProUGUI historyText;
    public TextMeshProUGUI labResultsText;
    public TextMeshProUGUI medicationsListText;

    public void UpdateUI(PatientCaseData patientData)
    {
        patientInfoText.text = $"Name: {patientData.patientName}\nAge: {patientData.age}\nWeight: {patientData.weight} kg\nHeight: {patientData.height} cm";
        historyText.text = $"Complaint: {patientData.chiefComplaint}\n\nHistory:\n{patientData.history}";
        labResultsText.text = patientData.labResults;
        medicationsListText.text = patientData.medicationsList;
    }
}
