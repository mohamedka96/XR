using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MedicalFilePageFlipper : MonoBehaviour
{
    public GameObject[] pages;  // ضع الصفحات هنا بالترتيب
    private int currentPage = 0;

    void Start()
    {
        ShowPage(currentPage);
    }

    public void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            ShowPage(currentPage);
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            ShowPage(currentPage);
        }
    }

    void ShowPage(int index)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == index);
        }
    }
}
