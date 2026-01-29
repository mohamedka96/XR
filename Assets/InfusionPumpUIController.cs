using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Text;

public class InfusionPumpUIController : MonoBehaviour
{
    public TextMeshProUGUI infusionListText;

    public void UpdateUI(PatientCaseData data)
    {
        if (data == null) return;

        // استخدام اسم المتغير الجديد (infusionPumps)
        if (data.infusionPumps != null && data.infusionPumps.Count > 0)
        {
            // بناء نص واحد من قائمة الأدوية
            StringBuilder sb = new StringBuilder();
            foreach (string pump in data.infusionPumps)
            {
                sb.AppendLine("- " + pump);
            }
            infusionListText.text = sb.ToString();
        }
        else
        {
            infusionListText.text = "No active infusions.";
        }
    }
}
