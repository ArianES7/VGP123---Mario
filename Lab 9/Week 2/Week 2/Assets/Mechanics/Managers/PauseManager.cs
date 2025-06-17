using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [Header("Drag in your HUD Panel here (contains the Pause button)")]
    public GameObject hudPanel;

    [Header("Drag in your Pause Menu Panel here (contains Quit/Resume buttons)")]
    public GameObject pauseMenuPanel;

    bool isPaused = false;

    void Start()
    {
        // Ensure we start unpaused, HUD visible, menu hidden
        Time.timeScale = 1f;
        hudPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused) Resume();
        else Pause();
    }

    public void Pause()
    {
        // hide HUD, show Pause Menu
        hudPanel.SetActive(false);
        pauseMenuPanel.SetActive(true);

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        // show HUD, hide Pause Menu
        pauseMenuPanel.SetActive(false);
        hudPanel.SetActive(true);

        Time.timeScale = 1f;
        isPaused = false;
    }


    /// <summary>Quit application (or stop playmode in editor)</summary>
    public void QuitGame()
    {
        Application.Quit();

    }
}