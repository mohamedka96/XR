using UnityEngine;
using TMPro;

public class HistoryFileUIController : MonoBehaviour
{
    public TextMeshProUGUI historyText;
    public TextMeshProUGUI medicationsText;
    public TextMeshProUGUI labsText;
    public TextMeshProUGUI imagingText;

    public void UpdateUI(PatientCaseData data)
    {
        if (data == null) return;

        // استخدام أسماء المتغيرات الجديدة
        historyText.text = data.medicalHistory;
        medicationsText.text = data.medicationList;
        labsText.text = data.labResults;
        imagingText.text = data.imagingReports;
    }
}
