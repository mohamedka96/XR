using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TMP_Text promptText;           // نص سؤال الطالب
    [SerializeField] TMP_Text patientText;          // نص إجابات المريض
    [SerializeField] Transform followupContainer;   // حاوية أزرار المتابعات
    [SerializeField] Button continueButton;         // زر المتابعة
    [SerializeField] GameObject followupButtonPrefab; // prefab لزر متابعة واحد
    [SerializeField] GameObject rootPanel;          // اللوحة/الكانفاس لإظهار/إخفاء الحوار

    [Header("Scenario")]
    [SerializeField] string jsonPath = "Scenario/ACS_NSTEMI_Dialogue"; // بدون .json

    ScenarioDef scenario;
    readonly Dictionary<string, Node> nodes = new Dictionary<string, Node>();
    Node current;
    readonly HashSet<int> clicked = new HashSet<int>();
    readonly List<GameObject> spawnedButtons = new List<GameObject>();

    void Awake()
    {
        if (continueButton != null)
        {
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(OnContinue);
            continueButton.interactable = false;
        }
        if (rootPanel != null) rootPanel.SetActive(false);
    }

    // اربط هذه الدالة مع XR Simple Interactable -> OnSelectEntered
    public void StartScenario()
    {
        if (!LoadScenario(jsonPath))
        {
            Debug.LogError($"DialogueManager: لم أستطع تحميل Resources/{jsonPath}.json");
            return;
        }
        if (rootPanel != null) rootPanel.SetActive(true);
        GoTo("greeting");
    }

    bool LoadScenario(string path)
    {
        TextAsset ta = Resources.Load<TextAsset>(path);
        if (ta == null) return false;

        scenario = JsonUtility.FromJson<ScenarioDef>(ta.text);
        nodes.Clear();
        if (scenario != null && scenario.nodes != null)
        {
            foreach (var n in scenario.nodes)
                if (!string.IsNullOrEmpty(n.id)) nodes[n.id] = n;
        }
        return nodes.Count > 0;
    }

    void GoTo(string id)
    {
        if (!nodes.TryGetValue(id, out current))
        {
            Debug.LogError($"DialogueManager: العُقدة '{id}' غير موجودة في JSON.");
            return;
        }

        promptText.text  = current.prompt  ?? "";
        patientText.text = current.patient ?? "";

        ClearFollowups();
        clicked.Clear();

        if (current.followups != null && current.followups.Length > 0)
        {
            continueButton.interactable = false;

            for (int i = 0; i < current.followups.Length; i++)
            {
                int index = i; // مهم لتجنّب مشكلة الإغلاق (closure)
                var f = current.followups[index];

                var go = Instantiate(followupButtonPrefab, followupContainer);
                spawnedButtons.Add(go);

                var btn   = go.GetComponent<Button>();
                var label = go.GetComponentInChildren<TMP_Text>();
                if (label) label.text = f.label;

                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() => OnFollowupClicked(index));
            }
        }
        else
        {
            continueButton.interactable = true;
        }
    }

    void OnFollowupClicked(int index)
    {
        if (current?.followups == null || index < 0 || index >= current.followups.Length) return;

        string ans = current.followups[index].answer;
        if (!string.IsNullOrEmpty(ans))
        {
            if (!string.IsNullOrEmpty(patientText.text)) patientText.text += "\n";
            patientText.text += "• " + ans;
        }

        clicked.Add(index);
        if (clicked.Count == current.followups.Length)
            continueButton.interactable = true;
    }

    void OnContinue()
    {
        if (current == null) return;

        if (string.IsNullOrEmpty(current.next) || current.next == "end")
        {
            // انتهاء السيناريو
            ClearFollowups();
            if (rootPanel != null) rootPanel.SetActive(false);
            return;
        }

        GoTo(current.next);
    }

    void ClearFollowups()
    {
        // احذف الأزرار التي أنشأناها
        for (int i = 0; i < spawnedButtons.Count; i++)
            if (spawnedButtons[i] != null) Destroy(spawnedButtons[i]);
        spawnedButtons.Clear();

        // احتياط إضافي
        if (followupContainer != null)
            for (int i = followupContainer.childCount - 1; i >= 0; i--)
                Destroy(followupContainer.GetChild(i).gameObject);
    }
}

[System.Serializable]
public class ScenarioDef
{
    public Node[] nodes;
}

[System.Serializable]
public class Node
{
    public string id;
    public string prompt;
    public string patient;
    public Followup[] followups;
    public string next; // ضع "end" عند آخر عقدة
}

[System.Serializable]
public class Followup
{
    public string label;
    public string answer;
}