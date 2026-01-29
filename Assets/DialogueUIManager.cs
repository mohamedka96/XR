using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DialogueUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dialogueContainer;
    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI dialogueLineText;
    public Button nextButton;
    public AudioSource audioSource;

    private ScenarioManager activeScenarioManager;

    void Start()
    {
        // الكود الأصلي: اربط الزر وأخفِ الواجهة
        nextButton.onClick.AddListener(OnNextButtonClicked);
        dialogueContainer.SetActive(false);
    }

    public void ShowDialogueEvent(ScenarioEvent eventToShow, ScenarioManager caller)
    {
        activeScenarioManager = caller;
        dialogueContainer.SetActive(true);

        // هذا هو الكود الذي كان يفشل بصمت
        speakerNameText.text = eventToShow.characterName;
        dialogueLineText.text = eventToShow.dialogueText;

        if (eventToShow.dialogueAudio != null && audioSource != null)
        {
            audioSource.clip = eventToShow.dialogueAudio;
            audioSource.Play();
        }
    }

    public void HideDialogue()
    {
        dialogueContainer.SetActive(false);
        if (audioSource != null) audioSource.Stop();
    }

    private void OnNextButtonClicked()
    {
        if (activeScenarioManager != null)
        {
            activeScenarioManager.DisplayNextEvent();
        }
    }
}
