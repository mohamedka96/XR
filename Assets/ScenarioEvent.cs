using UnityEngine;

// هذا الملف يحتوي فقط على تعريفات مشتركة

public enum ScenarioEventType { Dialogue, Task }

[System.Serializable]
public class ScenarioEvent
{
    public ScenarioEventType eventType;
    public string characterName;
    [TextArea(3, 10)]
    public string dialogueText;
    public AudioClip dialogueAudio;
    public GameObject taskObject;
}

