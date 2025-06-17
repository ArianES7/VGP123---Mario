using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private const int TitleSceneBuildIndex = 0;
    private const int MainSceneBuildIndex = 1;

    public int Score { get; private set; }
    public int Coins { get; private set; }
    public int Lives { get; private set; }

    public bool IsMarioBig { get; private set; } = false;
    public void SetMarioBig(bool big) => IsMarioBig = big;

    public event Action<int, int, int> OnHUDChanged;
    public event Action OnGameOver;

    [SerializeField] private GameObject playerPrefab;

    private GameObject _gameOverCanvas;
    private bool _isGameOver = false;

    protected override void Awake()
    {
        base.Awake();

        Lives = 3;
        Score = 0;
        Coins = 0;

        SceneManager.sceneLoaded += OnSceneLoaded;
        OnGameOver += HandleGameOver;

    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        OnGameOver -= HandleGameOver;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == MainSceneBuildIndex)
        {
            _gameOverCanvas = GameObject.Find("GameOver_Panel");
            if (_gameOverCanvas != null)
            {
                _gameOverCanvas.SetActive(false);
            }

            _isGameOver = false;
        }
    }

    private void Update()
    {
        if (_isGameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(TitleSceneBuildIndex);
            _isGameOver = false;
        }
    }

    public void AddScore(int points)
    {
        Score += points;
        OnHUDChanged?.Invoke(Score, Coins, Lives);
    }

    public void TriggerGameOver()
    {
        OnGameOver?.Invoke();
    }

    public void StartNewGame()
    {
        //ResetStats();
        SceneManager.LoadScene(MainSceneBuildIndex);
    }

    public void InstantiatePlayer(Vector3 spawnPos)
    {
        if (playerPrefab != null)
        {
            Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Player prefab not assigned in GameManager!");
        }
    }

    private void HandleGameOver()
    {
        if (_gameOverCanvas != null)
        {
            _gameOverCanvas.SetActive(true);
        }

        _isGameOver = true;
    }
    public void AddLife()
    {
        Lives++;
        OnHUDChanged?.Invoke(Score, Coins, Lives);
    }

    private void ResetStats()
    {
        Lives = 3;
        Score = 0;
        Coins = 0;
        IsMarioBig = false;
        OnHUDChanged?.Invoke(Score, Coins, Lives);
    }
}
