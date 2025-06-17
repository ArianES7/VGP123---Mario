using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour
{
    [Header("HUD Texts (TMP)")]
    public TMP_Text scoreText;
    public TMP_Text coinsText;
    public TMP_Text livesText;

    [Header("Game Over Panel")]
    public GameObject gameOverPanel;

    void Start()
    {
        // Make sure your TMP_Text references are assigned
        if (scoreText == null || coinsText == null || livesText == null || gameOverPanel == null)
        {
            Debug.LogError("HUDController: One or more UI references are null!", this);
            enabled = false;
            return;
        }

        // Subscribe to GameManager events
        var gm = GameManager.Instance;
        if (gm == null)
        {
            Debug.LogError("HUDController: No GameManager instance found!", this);
            enabled = false;
            return;
        }

        gm.OnHUDChanged += RefreshHUD;
        gm.OnGameOver   += ShowGameOver;

        // Initialize HUD
        RefreshHUD(gm.Score, gm.Coins, gm.Lives);
        gameOverPanel.SetActive(false);
    }

    void OnDestroy()
    {
        // Unsubscribe safely
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnHUDChanged -= RefreshHUD;
            GameManager.Instance.OnGameOver   -= ShowGameOver;
        }
    }

    private void RefreshHUD(int score, int coins, int lives)
    {
        scoreText.text = $"Score: {score:000000}";
        coinsText.text = $"Coins: {coins}";
        livesText.text = $"Lives: {lives}";
    }

    private void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
