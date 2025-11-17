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

    [Header("UI Canvases")]
    // مراجع لواجهات الأجهزة نفسها لإخفائها أو إظهارها
    public GameObject vitalSignsCanvas;
    public GameObject ventilatorCanvas;
    public GameObject infusionPumpCanvas;
    public GameObject historyFileCanvas;

    // --- جديد: قسم إعدادات السيناريو ---
    [Header("Scenario Settings")]
    [Tooltip("اسحب هنا الكائن الأب الذي يحتوي على شخصيات سيناريو أمراض الكلى")]
    public GameObject nephrologyCharacters; // مرجع لمجموعة شخصيات هذا السيناريو

    [Tooltip("اسحب هنا كائن ScenarioManager الخاص بسيناريو أمراض الكلى")]
    public ScenarioManager nephrologyScenario;

    // يمكنك إضافة مراجع لسيناريوهات أخرى هنا في المستقبل
    // public GameObject anotherScenarioCharacters;
    // public ScenarioManager anotherScenarioManager;
    // ------------------------------------

    void Start()
    {
        // في البداية، قم بإخفاء جميع واجهات الأجهزة
        ClearAllMonitors();

        // وقم بإخفاء كل مجموعات الشخصيات
        if (nephrologyCharacters != null) nephrologyCharacters.SetActive(false);
        // if (anotherScenarioCharacters != null) anotherScenarioCharacters.SetActive(false);
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
        if (vitalSignsUI != null) vitalSignsUI.UpdateUI(selectedCase);
        if (ventilatorUI != null) ventilatorUI.UpdateUI(selectedCase);
        if (infusionPumpUI != null) infusionPumpUI.UpdateUI(selectedCase);
        if (historyFileUI != null) historyFileUI.UpdateUI(selectedCase);

        // --- جديد: منطق تفعيل السيناريو ---
        // قم بإخفاء كل مجموعات الشخصيات أولاً (كإجراء احترازي قبل إظهار المجموعة الصحيحة)
        if (nephrologyCharacters != null) nephrologyCharacters.SetActive(false);
        // if (anotherScenarioCharacters != null) anotherScenarioCharacters.SetActive(false);

        // تحقق من الحالة المختارة لتفعيل السيناريو المناسب
        // caseIndex == 1 يعني الحالة الثانية في القائمة (لأن الترقيم يبدأ من 0)
        if (caseIndex == 1) 
        {
            // أظهر الشخصيات الخاصة بهذا السيناريو
            if (nephrologyCharacters != null)
            {
                nephrologyCharacters.SetActive(true);
                Debug.Log("Nephrology characters activated.");
            }

            // ابدأ السيناريو
            if (nephrologyScenario != null)
            {
                nephrologyScenario.StartScenario();
                Debug.Log("Nephrology scenario started.");
            }
        }
        // يمكنك إضافة شروط else if لسيناريوهات أخرى
        // else if (caseIndex == 0)
        // {
        //     // فعل شخصيات وسيناريو الحالة الأولى
        // }
        // ------------------------------------
    }

    // دالة لمسح جميع الشاشات (باستخدام الطريقة الصحيحة)
    public void ClearAllMonitors()
    {
        // بدلاً من تعطيل الكائن، نقوم بتعطيل مكون Canvas نفسه
        if (vitalSignsCanvas != null) vitalSignsCanvas.GetComponent<Canvas>().enabled = false;
        if (ventilatorCanvas != null) ventilatorCanvas.GetComponent<Canvas>().enabled = false;
        if (infusionPumpCanvas != null) infusionPumpCanvas.GetComponent<Canvas>().enabled = false;
        if (historyFileCanvas != null) historyFileCanvas.GetComponent<Canvas>().enabled = false;
    }
}
