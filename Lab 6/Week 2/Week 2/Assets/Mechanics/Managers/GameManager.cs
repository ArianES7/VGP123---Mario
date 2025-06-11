using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);  // Prevent duplicates
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);  // Optional: keep GameManager alive across scenes
    }


    public delegate PlayerController PlayerControllerDelegate(PlayerController playerInstance);
    public event PlayerControllerDelegate OnPlayerControllerCreated;
    public event Action<int> OnLivesChanged;

    #region Player Controller Info
    [SerializeField] private PlayerController playerPrefab;
    private PlayerController playerInstance;
    public PlayerController PlayerInstance => playerInstance;
    private Vector3 currentCheckpoint;
    #endregion

    #region Game Stats
    public int maxLives = 5;
    private int lives = 3;

    public int Lives
    {
        get => lives;
        set
        {
            if (value < 0)
            {
                GameOver();
                return;
            }

            if (lives > value)
            {
                Respawn();
            }

            lives = value;
            OnLivesChanged?.Invoke(lives);
            Debug.Log("Lives have been set to: " + lives);
        }
    }
    #endregion

    void Start()
    {
        if (maxLives <= 0)
            maxLives = 5;

        Lives = maxLives;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            string currentScene = SceneManager.GetActiveScene().name;
            string nextScene = currentScene == "Game" ? "Title" : "Game";
            SceneManager.LoadScene(nextScene);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Lives--;
        }
    }

    private void Respawn()
    {
        Debug.Log("Respawning player...");
        if (playerInstance != null)
        {
            playerInstance.transform.position = currentCheckpoint;
            // Optional: Add animation or effect here
        }
        else
        {
            Debug.LogWarning("No player instance found to respawn.");
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over triggered.");
        // Add game over logic: UI, sound, scene transition, etc.
    }

    public void InstantiatePlayer(Vector3 spawnPos)
    {
        if (playerInstance != null)
        {
            Destroy(playerInstance.gameObject);
        }

        playerInstance = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        currentCheckpoint = spawnPos;

        OnPlayerControllerCreated?.Invoke(playerInstance);
    }

    public void SetCheckpoint(Vector3 checkpointPos)
    {
        currentCheckpoint = checkpointPos;
        Debug.Log("Checkpoint updated to: " + currentCheckpoint);
    }
}
