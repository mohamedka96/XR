using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewPatientCase", menuName = "Patient Data/Patient Case Data")]
public class PatientCaseData : ScriptableObject
{
    [Header("1. Case Identification")]
    [Tooltip("الاسم الذي سيظهر على زر اختيار الحالة في القائمة الرئيسية.")]
    public string caseName = "New Medical Case";

    [Header("2. Patient Demographics")]
    public string patientName = "John Doe";
    public int age = 45;
    public string gender = "Male";
    public float weight = 70f;
    public float height = 175f;

    [Header("3. Admission Details")]
    [TextArea(3, 5)]
    public string chiefComplaint = "N/A";
    [TextArea(3, 5)]
    public string admissionDiagnosis = "N/A";

    [Header("4. Vital Signs Monitor")]
    public string bloodPressure = "120/80";
    public int heartRate = 80;
    public int respiratoryRate = 18;
    public float temperature = 37.0f;
    public int spO2 = 98;

    [Header("5. Ventilator")]
    public string ventilationMode = "ACVC";
    public int ventRespiratoryRate = 12;
    public int tidalVolume = 500;
    public float fio2 = 0.4f;
    public int peep = 5;
    public int peakPressure = 25;
    public int plateauPressure = 20;

    [Header("6. Infusion Pumps")]
    [Tooltip("قائمة بالأدوية التي يتم ضخها حاليًا.")]
    public List<string> infusionPumps = new List<string>();

    [Header("7. Urine Output")]
    public float urineOutput = 50f; // mL/hr

    [Header("8. Patient History File")]
    [TextArea(10, 15)]
    public string medicalHistory = "No significant past medical history.";
    [TextArea(5, 10)]
    public string medicationList = "No regular medications.";
    [TextArea(5, 10)]
    public string labResults = "Pending.";
    [TextArea(5, 10)]
    public string imagingReports = "No recent imaging.";

}
