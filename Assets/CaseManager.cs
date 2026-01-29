using UnityEngine;
using System.Collections.Generic;

public class CaseManager : MonoBehaviour
{
    [Header("Case Data")]
    public List<PatientCaseData> patientCases;

    [Header("Scene Controllers")]
    public List<ScenarioManager> scenarioManagers;
    public List<GameObject> characterGroups;

    [Header("Device UI Controllers")]
    public VitalSignsUIController vitalSignsUI;
    public VentilatorUIController ventilatorUI;
    public InfusionPumpUIController infusionPumpUI;
    public HistoryFileUIController historyFileUI;

    // متغير لتخزين السيناريو النشط حاليًا
    private ScenarioManager activeScenario = null;

    void Start()
    {
        // عند البدء، تأكد من أن كل شيء مخفي
        foreach (var group in characterGroups)
        {
            if (group != null) group.SetActive(false);
        }
        // لا نقم بإيقاف السيناريوهات هنا لتجنب المشاكل
    }

    public void SelectCase(int caseIndex)
    {
        if (caseIndex < 0 || caseIndex >= patientCases.Count) return;

        // --- المنطق الجديد والآمن ---

        // الخطوة 1: أوقف السيناريو القديم (إذا كان هناك واحد) وأخفِ كل الشخصيات.
        if (activeScenario != null)
        {
            activeScenario.StopScenario();
            activeScenario = null;
        }
        foreach (var group in characterGroups)
        {
            if (group != null) group.SetActive(false);
        }

        // الخطوة 2: حدث بيانات الأجهزة.
        PatientCaseData selectedCase = patientCases[caseIndex];
        vitalSignsUI.UpdateUI(selectedCase);
        ventilatorUI.UpdateUI(selectedCase);
        infusionPumpUI.UpdateUI(selectedCase);
        historyFileUI.UpdateUI(selectedCase);

        // الخطوة 3: أظهر الشخصيات الجديدة وابدأ السيناريو الجديد.
        if (caseIndex < characterGroups.Count && characterGroups[caseIndex] != null)
        {
            characterGroups[caseIndex].SetActive(true);
        }

        if (caseIndex < scenarioManagers.Count && scenarioManagers[caseIndex] != null)
        {
            activeScenario = scenarioManagers[caseIndex];
            activeScenario.StartScenario();
        }
    }
}
