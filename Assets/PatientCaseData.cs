using UnityEngine;

// هذا السطر يسمح لنا بإنشاء ملفات من هذا النوع مباشرة من قائمة "Create" في محرر Unity
[CreateAssetMenu(fileName = "NewPatientCase", menuName = "ICU/Patient Case Data")]
public class PatientCaseData : ScriptableObject
{
    // === معلومات المريض الأساسية ===
    [Header("Patient Information")]
    public string patientName;
    public int age;
    public float weight;
    public float height;
    public string chiefComplaint;
    [TextArea(5, 10)] // يجعل حقل النص أكبر في الـ Inspector لسهولة الكتابة
    public string history;

    // === قراءات شاشة العلامات الحيوية ===
    [Header("Vital Signs")]
    public string bloodPressure; // كنص "152/90" لسهولة العرض
    public int heartRate;
    public int respiratoryRate;
    public int spo2;
    public float temperature;

    // === قراءات جهاز قياس إخراج البول ===
    [Header("Urine Output")]
    public float urineOutputPerHour;

    // === إعدادات جهاز التنفس الصناعي ===
    [Header("Ventilator Settings")]
    public int ventilatorRate;      // معدل التنفس المحدد على الجهاز
    public float tidalVolume;       // حجم الهواء
    public float peep;              // ضغط نهاية الزفير الإيجابي
    public float fio2;              // نسبة الأكسجين (تكتب كقيمة بين 0.0 و 1.0، مثلا 0.85 لـ 85%)

    // === إعدادات مضخة التسريب ===
    [Header("Infusion Pump")]
    public string infusionDrugName; // اسم الدواء
    public float infusionRate;      // معدل التسريب (ml/hr)
    public float infusionVTBI;      // الحجم الكلي المراد إعطاؤه (Volume To Be Infused)

    // === بيانات ملف التاريخ المرضي ===
    [Header("History File Data")]
    [TextArea(8, 15)] // حقل أكبر لعرض نتائج المختبر
    public string labResults;

    [TextArea(8, 15)] // حقل أكبر لعرض قائمة الأدوية
    public string medicationsList;
}
