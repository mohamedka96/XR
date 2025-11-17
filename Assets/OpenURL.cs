using UnityEngine;

public class OpenURL : MonoBehaviour
{
    // هذه الدالة ستقوم بفتح الرابط المحدد
    public void Open(string url)
    {
        // تأكد من أن الرابط ليس فارغاً
        if (!string.IsNullOrEmpty(url))
        {
            // الأمر الرئيسي لفتح الرابط في المتصفح الافتراضي
            Application.OpenURL(url);
            Debug.Log("Opening URL: " + url);
        }
        else
        {
            Debug.LogError("URL is empty or null!");
        }
    }
}
