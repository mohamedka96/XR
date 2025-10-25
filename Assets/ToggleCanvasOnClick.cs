using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // مهم جداً إضافة هذا السطر

public class ToggleCanvasOnClick : MonoBehaviour
{
    public GameObject targetCanvas; // هذه الخانة تبقى كما هي في Inspector

    // هذه الدالة سنربطها يدوياً في Inspector
    public void ToggleVisibility()
    {
        if (targetCanvas != null)
        {
            Canvas canvasComponent = targetCanvas.GetComponent<Canvas>();
            if (canvasComponent != null)
            {
                // اعكس حالة تفعيل مكون Canvas
                canvasComponent.enabled = !canvasComponent.enabled;
                Debug.Log("Toggled Canvas: " + targetCanvas.name); // لنتأكد أن الدالة استدعيت
            }
        }
        else
        {
            Debug.LogError("Target Canvas is not assigned!");
        }
    }
}
