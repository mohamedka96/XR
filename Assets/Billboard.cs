using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform mainCameraTransform;

    void Start()
    {
        // البحث عن الكاميرا الرئيسية وتخزينها لتجنب البحث عنها في كل إطار
        if (Camera.main != null)
        {
            mainCameraTransform = Camera.main.transform;
        }
    }

    void LateUpdate()
    {
        // التأكد من وجود الكاميرا قبل محاولة استخدامها
        if (mainCameraTransform == null)
        {
            return;
        }

        // اجعل الواجهة تنظر إلى الكاميرا
        // نستخدم نفس اتجاه الكاميرا الأمامي
        transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward,
                         mainCameraTransform.rotation * Vector3.up);
    }
}
