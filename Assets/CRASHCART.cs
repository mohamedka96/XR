using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CRASHCART : MonoBehaviour
{
    public Vector3 openOffset = new Vector3(0, 0, 0.3f);
    private Vector3 closedPosition;
    private bool isOpen = false;

    void Start()
    {
        closedPosition = transform.localPosition;
    }

    public void ToggleDrawer()
    {
        if (isOpen)
        {
            transform.localPosition = closedPosition;
        }
        else
        {
            transform.localPosition = closedPosition + openOffset;
        }

        isOpen = !isOpen;
    }
}