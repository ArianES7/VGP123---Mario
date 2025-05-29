using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate PlayerController PlayerControllerDelegate(PlayerController playerInstance);
    public event PlayerControllerDelegate OnPlayerControllerCreated;

    #region Singleton Pattern
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }
    #endregion

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
        get
        {
            return lives;
        }
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
            Debug.Log("Lives have been set to: " + lives);
        }
    }
    #endregion

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize lives or any other game state here
        if (maxLives <= 0)
            maxLives = 5; // Default value if not set

        Lives = maxLives;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            string currentSceneName = SceneManager.GetActiveScene().name;

            string loadedSceneName = currentSceneName == "Game" ? "Title" : "Game";
            SceneManager.LoadScene(loadedSceneName);
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Lives--;
            //Debug.Log("Lives decremented, current lives: " + lives);
        }
    }

    private void Respawn()
    {
        Debug.Log("Respawn goes here");
        //TODO: Respawn animation then reset player position
        playerInstance.transform.position = currentCheckpoint;
    }

    private void GameOver()
    {
        Debug.Log("Game Over goes here");
    }

    public void InstantatePlayer(Vector3 spawnPos)
    {
        playerInstance = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
        currentCheckpoint = spawnPos;

        //This is where we invoke the event to notify other systems that the player controller has been created
        //if (OnPlayerControllerCreated != null) is the same as OnPlayerControllerCreated?.Invoke(playerInstance);
        //? is the null-conditional operator, which checks if OnPlayerControllerCreated is not null before invoking it.
        OnPlayerControllerCreated?.Invoke(playerInstance);
    }

    public void SetCheckpoint(Vector3 checkpointPos)
    {
        currentCheckpoint = checkpointPos;
        Debug.Log("Checkpoint set at: " + currentCheckpoint);
    }
}
