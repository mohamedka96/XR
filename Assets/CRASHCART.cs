using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class CRASHCART : MonoBehaviour
{
    public Vector3 openOffset = new Vector3(0, 0, 0.3f);
    public float openSpeed = 2f; // سرعة فتح الدرج (يمكنك تغيير القيمة من الـInspector)
    
    private Vector3 closedPosition;
    private bool isOpen = false;
    private bool isMoving = false;

    void Start()
    {
        closedPosition = transform.localPosition;
    }

    public void ToggleDrawer()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveDrawer());
        }
    }

    private IEnumerator MoveDrawer()
    {
        isMoving = true;
        
        Vector3 startPosition = transform.localPosition;
        Vector3 targetPosition = isOpen ? closedPosition : closedPosition + openOffset;
        
        float elapsedTime = 0f;
        float duration = 1f / openSpeed;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            
            // استخدام Lerp للحركة السلسة
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
            
            yield return null;
        }

        // التأكد من الوصول للموضع النهائي بدقة
        transform.localPosition = targetPosition;
        isOpen = !isOpen;
        isMoving = false;
    }
}