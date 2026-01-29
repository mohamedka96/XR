using UnityEngine;
using System.Collections.Generic;

public class ScenarioManager : MonoBehaviour
{
    [Header("Scenario Data")]
    public List<ScenarioEvent> scenarioEvents;

    [Header("Dependencies")]
    [Tooltip("اسحب هنا كائن Dialogue_UI الذي يحتوي على سكربت DialogueUIManager")]
    public DialogueUIManager dialogueUIManager; // <-- هذا هو الحقل الجديد والمهم

    private int currentEventIndex = 0;

    // لم نعد بحاجة لدالة Start() للاكتشاف التلقائي

    public void StartScenario()
    {
        if (dialogueUIManager == null)
        {
            Debug.LogError($"ScenarioManager ({this.name}): DialogueUIManager is not assigned in the Inspector! Cannot start scenario.");
            return;
        }
        if (scenarioEvents == null || scenarioEvents.Count == 0) return;
        
        currentEventIndex = 0;
        DisplayEvent(currentEventIndex);
    }

    public void StopScenario()
    {
        if (dialogueUIManager != null)
        {
            dialogueUIManager.HideDialogue();
        }
    }

    public void DisplayNextEvent()
    {
        currentEventIndex++;
        if (currentEventIndex < scenarioEvents.Count)
        {
            DisplayEvent(currentEventIndex);
        }
        else
        {
            StopScenario();
        }
    }

    private void DisplayEvent(int eventIndex)
    {
        ScenarioEvent currentEvent = scenarioEvents[eventIndex];

        if (currentEvent.eventType == ScenarioEventType.Dialogue)
        {
            dialogueUIManager.ShowDialogueEvent(currentEvent, this);
        }
        else if (currentEvent.eventType == ScenarioEventType.Task)
        {
            dialogueUIManager.HideDialogue();
            if (currentEvent.taskObject != null)
            {
                currentEvent.taskObject.SetActive(true);
            }
        }
    }
}

