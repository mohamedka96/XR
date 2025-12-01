using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("إعدادات القائمة")]
    public GameObject pauseMenuCanvas;
    public GameObject rightHandController;

    private MyGame_InputActions inputActions; // --- جديد: مرجع لكلاس الإدخال
    private bool isPaused = false;

    void Awake()
    {
        // --- جديد: تهيئة نظام الإدخال ---
        inputActions = new MyGame_InputActions();
    }

    void OnEnable()
    {
        // --- جديد: تفعيل الإجراءات ---
        inputActions.PlayerControls.Pause.performed += TogglePause;
        inputActions.PlayerControls.Enable();
    }

    void OnDisable()
    {
        // --- جديد: تعطيل الإجراءات ---
        inputActions.PlayerControls.Disable();
        inputActions.PlayerControls.Pause.performed -= TogglePause;
    }

    void Start()
    {
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(false);
        }
    }

    // لاحظ أن هذه الدالة لم تعد تحتاج إلى مرجع في Inspector
    private void TogglePause(InputAction.CallbackContext context)
    {
        Debug.Log("!!! NEW SYSTEM: TogglePause function was called! !!!");

        isPaused = !isPaused;

        if (isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }

    // ... بقية الدوال (ActivateMenu, DeactivateMenu, etc.) تبقى كما هي ...
    void ActivateMenu()
    {
        Time.timeScale = 0f;
        if (pauseMenuCanvas != null && rightHandController != null)
        {
            pauseMenuCanvas.transform.position = rightHandController.transform.position + rightHandController.transform.forward * 1.5f;
            pauseMenuCanvas.transform.rotation = Quaternion.LookRotation(rightHandController.transform.forward);
            pauseMenuCanvas.SetActive(true);
        }
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1f;
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.SetActive(false);
        }
        isPaused = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
