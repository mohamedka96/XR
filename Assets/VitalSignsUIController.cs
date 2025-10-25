using UnityEngine;
using TMPro; // لا تنس إضافة هذا السطر لاستخدام TextMeshPro

public class VitalSignsUIController : MonoBehaviour
{
    [Header("UI Text Fields")]
    public TextMeshProUGUI bloodPressureText;
    public TextMeshProUGUI heartRateText;
    public TextMeshProUGUI respiratoryRateText;
    public TextMeshProUGUI spo2Text;
    public TextMeshProUGUI temperatureText;

    // هذه الدالة ستقوم بتحديث الواجهة بناءً على بيانات الحالة المرضية
    public void UpdateUI(PatientCaseData patientData)
    {
        // التأكد من أن البيانات ليست فارغة
        if (patientData == null)
        {
            Debug.LogError("Patient data is null!");
            return;
        }

        // تحديث النصوص بالبيانات من الـ ScriptableObject
        bloodPressureText.text = patientData.bloodPressure;
        heartRateText.text = patientData.heartRate.ToString();
        respiratoryRateText.text = patientData.respiratoryRate.ToString();
        spo2Text.text = patientData.spo2.ToString() + "%"; // نضيف علامة النسبة المئوية
        temperatureText.text = patientData.temperature.ToString("F1") + " °C"; // نعرض فاصلة عشرية واحدة
    }

    // يمكنك استخدام هذه الدالة لمسح البيانات من الشاشة
    public void ClearUI()
    {
        bloodPressureText.text = "--/--";
        heartRateText.text = "--";
        respiratoryRateText.text = "--";
        spo2Text.text = "--";
        temperatureText.text = "--";
    }
}
