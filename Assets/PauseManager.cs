using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("واجهة قائمة الإيقاف التي سيتم إظهارها وإخفاؤها.")]
    public GameObject pauseMenuCanvas;

    [Tooltip("إجراء الإدخال الخاص بزر القائمة (يجب أن يكون مربوطًا بلوحة المفاتيح ويد التحكم).")]
    public InputActionReference menuButtonAction;

    private bool isPaused = false;

    private void OnEnable()
    {
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(false);
        }

        if (menuButtonAction != null)
        {
            menuButtonAction.action.Enable();
            menuButtonAction.action.performed += TogglePause;
            Debug.Log("Pause action enabled and subscribed.");
        }
        else
        {
            Debug.LogError("Menu Button Action is not assigned in the Inspector!");
        }
    }

    private void OnDisable()
    {
        if (menuButtonAction != null)
        {
            menuButtonAction.action.performed -= TogglePause;
        }
    }

    private void TogglePause(InputAction.CallbackContext context)
    {
        Debug.Log("TogglePause action performed!");
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Game Paused");
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("Game Resumed");
    }

    public void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitApplication()
    {
        Debug.Log("Quitting application...");
        Application.Quit();
    }
}
