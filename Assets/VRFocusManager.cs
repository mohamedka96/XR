using UnityEngine;
using UnityEngine.XR.Management; // مطلوب للتحكم في نظام XR

public class VRFocusManager : MonoBehaviour
{
    // هذه الدالة يتم استدعاؤها تلقائيًا من Unity
    // onApplicationFocus(true) -> عند العودة إلى التطبيق
    // onApplicationFocus(false) -> عند مغادرة التطبيق
    void OnApplicationFocus(bool hasFocus)
    {
        // إذا كنا قد عدنا للتو إلى التطبيق
        if (hasFocus)
        {
            Debug.Log("Application regained focus. Attempting to reinitialize XR...");
            
            // أوقف نظام XR أولاً
            if (XRGeneralSettings.Instance.Manager.isInitializationComplete)
            {
                XRGeneralSettings.Instance.Manager.StopSubsystems();
            }

            // ثم أعد تشغيله
            XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
            XRGeneralSettings.Instance.Manager.StartSubsystems();

            Debug.Log("XR reinitialization complete.");
        }
        else
        {
            Debug.Log("Application lost focus.");
        }
    }
}
