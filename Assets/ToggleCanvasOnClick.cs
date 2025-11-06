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
            // نعكس حالة تفعيل GameObject بالكامل بدلاً من مكون Canvas فقط
            bool newState = !targetCanvas.activeSelf;
            targetCanvas.SetActive(newState);
            Debug.Log("Toggled Canvas: " + targetCanvas.name + " - " + (newState ? "Shown" : "Hidden"));
        }
        else
        {
            Debug.LogError("Target Canvas is not assigned!");
        }
    }

    // دالة لإظهار الشاشة
    public void ShowCanvas()
    {
        if (targetCanvas != null)
        {
            targetCanvas.SetActive(true);
            Debug.Log("Showed Canvas: " + targetCanvas.name);
        }
        else
        {
            Debug.LogError("Target Canvas is not assigned!");
        }
    }

    // دالة لإخفاء الشاشة
    public void HideCanvas()
    {
        if (targetCanvas != null)
        {
            targetCanvas.SetActive(false);
            Debug.Log("Hid Canvas: " + targetCanvas.name);
        }
        else
        {
            Debug.LogError("Target Canvas is not assigned!");
        }
    }
}