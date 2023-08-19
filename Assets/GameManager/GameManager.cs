using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Waiting,
    Playing,
    TimeIsUp
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private float playTime;
    
    private GameState gameState;
    private float timer;
    private bool gameStarted;

    public GameState GameState => gameState;
    public float TimeLeft => playTime - timer;

    public static GameManager Instance;
    
    public Action OnGameStarted;
    public Action OnTimeIsUp;

    private void Awake()
    {
        gameState = GameState.Waiting;

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        ItemDatabase.Awake();
    }

    private void Start()
    {
        PlayerManagement.Instance.OnPlayerJoinedCallback += OnPlayerJoinedCallback;
    }

    private void OnPlayerJoinedCallback(Player player)
    {
        if (GameState == GameState.Waiting && PlayerManagement.Instance.PlayerAmount == 2)
        {
            LKWManager.Instance.StartGame();
        }
    }

    private void Update()
    {
        if (gameState == GameState.TimeIsUp)
        {
            return;
        }

        if (!gameStarted) return;
        
        timer += Time.deltaTime;

        if (!(timer > playTime)) return;
        
        gameState = GameState.TimeIsUp;
        OnTimeIsUp?.Invoke();
    }
    

    public void StartGame()
    {
        gameStarted = true;
        gameState = GameState.Playing;
        OnGameStarted?.Invoke();
    }
}
