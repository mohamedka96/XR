using UnityEngine;

public class CaseManager : MonoBehaviour
{
    [Header("Patient Case Data Files")]
    // قائمة لتضع فيها ملفات ScriptableObject الخاصة بالحالات
    public PatientCaseData[] patientCases;

    [Header("UI Controllers")]
    // مراجع لجميع سكربتات التحكم بواجهات الأجهزة
    public VitalSignsUIController vitalSignsUI;
    public VentilatorUIController ventilatorUI;
    public InfusionPumpUIController infusionPumpUI;
    public HistoryFileUIController historyFileUI;
    // يمكنك إضافة المزيد من أجهزة التحكم هنا في المستقبل

    [Header("UI Canvases")]
    // مراجع لواجهات الأجهزة نفسها لإظهارها أو إخفائها
    public GameObject vitalSignsCanvas;
    public GameObject ventilatorCanvas;
    public GameObject infusionPumpCanvas;
    public GameObject historyFileCanvas;

    void Start()
    {
        // في البداية، قم بإخفاء جميع واجهات الأجهزة
        // واجعلها تعرض بيانات فارغة
        ClearAllMonitors();
    }

    // هذه الدالة سيتم استدعاؤها عند الضغط على أزرار القائمة
    public void SelectCase(int caseIndex)
    {
        // تحقق من أن الرقم ضمن نطاق الحالات المتاحة
        if (caseIndex < 0 || caseIndex >= patientCases.Length)
        {
            Debug.LogError("Invalid case index selected: " + caseIndex);
            return;
        }

        // احصل على بيانات الحالة المختارة من القائمة
        PatientCaseData selectedCase = patientCases[caseIndex];

        Debug.Log("Selected Case: " + selectedCase.patientName);

        // قم بتحديث جميع واجهات الأجهزة بالبيانات الجديدة
        vitalSignsUI.UpdateUI(selectedCase);
        ventilatorUI.UpdateUI(selectedCase);
        infusionPumpUI.UpdateUI(selectedCase);
        historyFileUI.UpdateUI(selectedCase);
    }

    // دالة لمسح جميع الشاشات
    public void ClearAllMonitors()
    {
        // هنا يمكنك إما مسح النصوص أو إخفاء الواجهات بالكامل
        // مثال على الإخفاء:
        if (vitalSignsCanvas != null) vitalSignsCanvas.SetActive(false);
        if (ventilatorCanvas != null) ventilatorCanvas.SetActive(false);
        if (infusionPumpCanvas != null) infusionPumpCanvas.SetActive(false);
        if (historyFileCanvas != null) historyFileCanvas.SetActive(false);
    }

    // دوال لإظهار شاشة جهاز معين عند الضغط عليه
    public void ShowVitalSignsMonitor()
    {
        if (vitalSignsCanvas != null)
        {
            vitalSignsCanvas.SetActive(true);
            Debug.Log("Vital Signs Monitor Shown");
        }
    }

    public void ShowVentilatorMonitor()
    {
        if (ventilatorCanvas != null)
        {
            ventilatorCanvas.SetActive(true);
            Debug.Log("Ventilator Monitor Shown");
        }
    }

    public void ShowInfusionPumpMonitor()
    {
        if (infusionPumpCanvas != null)
        {
            infusionPumpCanvas.SetActive(true);
            Debug.Log("Infusion Pump Monitor Shown");
        }
    }

    public void ShowHistoryFileMonitor()
    {
        if (historyFileCanvas != null)
        {
            historyFileCanvas.SetActive(true);
            Debug.Log("History File Monitor Shown");
        }
    }

    // دالة لإخفاء شاشة معينة
    public void HideVitalSignsMonitor()
    {
        if (vitalSignsCanvas != null) vitalSignsCanvas.SetActive(false);
    }

    public void HideVentilatorMonitor()
    {
        if (ventilatorCanvas != null) ventilatorCanvas.SetActive(false);
    }

    public void HideInfusionPumpMonitor()
    {
        if (infusionPumpCanvas != null) infusionPumpCanvas.SetActive(false);
    }

    public void HideHistoryFileMonitor()
    {
        if (historyFileCanvas != null) historyFileCanvas.SetActive(false);
    }

    // دالة لتبديل ظهور/إخفاء شاشة معينة
    public void ToggleVitalSignsMonitor()
    {
        if (vitalSignsCanvas != null)
        {
            bool newState = !vitalSignsCanvas.activeSelf;
            vitalSignsCanvas.SetActive(newState);
            Debug.Log("Vital Signs Monitor: " + (newState ? "Shown" : "Hidden"));
        }
    }

    public void ToggleVentilatorMonitor()
    {
        if (ventilatorCanvas != null)
        {
            bool newState = !ventilatorCanvas.activeSelf;
            ventilatorCanvas.SetActive(newState);
            Debug.Log("Ventilator Monitor: " + (newState ? "Shown" : "Hidden"));
        }
    }

    public void ToggleInfusionPumpMonitor()
    {
        if (infusionPumpCanvas != null)
        {
            bool newState = !infusionPumpCanvas.activeSelf;
            infusionPumpCanvas.SetActive(newState);
            Debug.Log("Infusion Pump Monitor: " + (newState ? "Shown" : "Hidden"));
        }
    }

    public void ToggleHistoryFileMonitor()
    {
        if (historyFileCanvas != null)
        {
            bool newState = !historyFileCanvas.activeSelf;
            historyFileCanvas.SetActive(newState);
            Debug.Log("History File Monitor: " + (newState ? "Shown" : "Hidden"));
        }
    }
}