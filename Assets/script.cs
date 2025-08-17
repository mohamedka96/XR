using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DrawerToggle : MonoBehaviour
{
    public Vector3 closedPosition;   // الموضع المغلق
    public Vector3 openPosition;     // الموضع المفتوح
    public float moveSpeed = 3f;     // سرعة الفتح والغلق

    private bool isOpen = false;
    private bool isMoving = false;
    private Vector3 targetPosition;

    private void Start()
    {
        closedPosition = transform.localPosition;
        // افتح بمقدار معين للأمام، حسب التصميم (مثلاً 0.5 متر للأمام)
        openPosition = closedPosition + new Vector3(0, 0, 0.5f); 
        targetPosition = closedPosition;
    }

    public void ToggleDrawer()
    {
        isOpen = !isOpen;
        targetPosition = isOpen ? openPosition : closedPosition;
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * moveSpeed);

            if (Vector3.Distance(transform.localPosition, targetPosition) < 0.01f)
            {
                transform.localPosition = targetPosition;
                isMoving = false;
            }
        }
    }
}
