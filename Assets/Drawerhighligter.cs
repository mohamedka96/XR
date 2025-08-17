using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DrawerHighlighter : MonoBehaviour
{
    private Renderer rend;
    private Color originalColor;

    public Color highlightColor = Color.cyan; // اللون عند التوهج

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    public void OnHoverEnter(HoverEnterEventArgs args)
    {
        rend.material.color = highlightColor;
    }

    public void OnHoverExit(HoverExitEventArgs args)
    {
        rend.material.color = originalColor;
    }
}