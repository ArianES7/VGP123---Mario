using UnityEngine;

public class TitleMenu : MonoBehaviour
{

    private void Start()
    {

        AudioManager.Instance.PlayMusic("MenuMusic");

    }


    /// <summary>
    /// Hook this up to your Title-scene Play button’s OnClick.
    /// </summary>
    public void OnPlayButtonPressed()
    {
        GameManager.Instance.StartNewGame();
        AudioManager.Instance.StopMusic();
    }

    /// <summary>
    /// (Optional) Hook this to a “Quit” button if desired.
    /// </summary>
    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
