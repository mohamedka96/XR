using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

// تعريف هيكل الحدث (مع دعم المهام التفاعلية)
[System.Serializable]
public class ScenarioEvent
{
    public string characterName;
    [TextArea(3, 5)]
    public string dialogueLine;
    public AudioClip dialogueAudio;
    
    [Header("Interactive Task")]
    public bool isInteractiveTask = false;
    public GameObject taskObject;
}

public class ScenarioManager : MonoBehaviour
{
    [Header("عناصر واجهة الحوار")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI dialogueLineText;
    public Button nextButton; // --- جديد: مرجع لزر "التالي" ---

    [Header("ملفات الصوت")]
    public AudioSource audioSource;

    [Header("أحداث السيناريو")]
    public ScenarioEvent[] events;

    private int currentEventIndex = 0;
    private bool isWaitingForUserInput = false; // --- جديد: متغير لتتبع حالة الانتظار ---

    void Start()
    {
        // تأكد من إخفاء اللوحة والزر في البداية
        if (dialoguePanel != null) dialoguePanel.SetActive(false);
        if (nextButton != null) nextButton.gameObject.SetActive(false);

        // ربط دالة OnNextButtonClicked بحدث النقر على الزر
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(OnNextButtonClicked);
        }
    }

    // هذه الدالة ستبدأ السيناريو بأكمله
    public void StartScenario()
    {
        if (dialoguePanel.activeSelf) return;

        currentEventIndex = 0;
        dialoguePanel.SetActive(true);
        StartCoroutine(PlayCurrentEvent());
    }

    // دالة يتم استدعاؤها عند النقر على زر "التالي"
    private void OnNextButtonClicked()
    {
        // إذا كنا في حالة انتظار، قم بتشغيل الحدث التالي
        if (isWaitingForUserInput)
        {
            currentEventIndex++;
            StartCoroutine(PlayCurrentEvent());
        }
    }

    // كوروتين جديد لتشغيل حدث واحد فقط في كل مرة
    private IEnumerator PlayCurrentEvent()
    {
        // تحقق من أننا لم نتجاوز نهاية السيناريو
        if (currentEventIndex >= events.Length)
        {
            EndScenario();
            yield break; // اخرج من الكوروتين
        }

        isWaitingForUserInput = false; // لسنا في حالة انتظار الآن
        nextButton.gameObject.SetActive(false); // أخفِ الزر أثناء عرض الحدث

        var currentEvent = events[currentEventIndex];

        // عرض بيانات الحدث الحالي
        characterNameText.text = currentEvent.characterName;
        dialogueLineText.text = currentEvent.dialogueLine;

        // --- منطق المهام التفاعلية (يبقى كما هو) ---
        if (currentEvent.isInteractiveTask)
        {
            if (currentEvent.taskObject != null)
            {
                currentEvent.taskObject.SetActive(true);
            }
            dialoguePanel.SetActive(false);
            // ملاحظة: المهمة التفاعلية مسؤولة عن استئناف السيناريو
            yield break;
        }

        // تشغيل الصوت أو الانتظار بناءً على النص
        if (currentEvent.dialogueAudio != null)
        {
            audioSource.PlayOneShot(currentEvent.dialogueAudio);
            yield return new WaitForSeconds(currentEvent.dialogueAudio.length);
        }
        else
        {
            float waitTime = currentEvent.dialogueLine.Length * 0.05f;
            yield return new WaitForSeconds(Mathf.Max(2.0f, waitTime));
        }

        // بعد انتهاء الحوار، أظهر الزر وادخل في حالة انتظار
        isWaitingForUserInput = true;
        nextButton.gameObject.SetActive(true);
    }

    // دالة لإنهاء السيناريو
    private void EndScenario()
    {
        dialoguePanel.SetActive(false);
        nextButton.gameObject.SetActive(false);
        Debug.Log("انتهى السيناريو!");
    }

    // --- دالة استئناف السيناريو بعد مهمة تفاعلية (معدلة قليلاً) ---
    public void ResumeScenarioAfterTask()
    {
        dialoguePanel.SetActive(true);
        currentEventIndex++; // انتقل إلى الحدث التالي بعد المهمة
        StartCoroutine(PlayCurrentEvent());
    }
}
