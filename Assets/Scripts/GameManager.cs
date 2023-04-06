using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton Pattern
    public static GameManager instance;

    // Game assets Tags
    public const string TITLE_SCENE_TAG = "Titlescreen";
    public const string GAMEPLAY_SCENE_TAG = "Gameplay";

    public const string PLAYER_TAG = "PlayerShip";
    public const string METEORITE_TAG = "Meteorite";
    public const string PROJECTILE_TAG = "Projectile";
    public const string CAGE_TAG = "Cage";

    // Game Settings and Values
    public const float PLAYER_BOOST_SPEED = 100.0f;
    public const float PLAYER_ROTATION_SPEED = 150.0f;

    public const int METEORITE_POINTS_VALUE = 50;
    public const float METEORITE_BIG_MIN_SPEED = 50f;
    public const float METEORITE_BIG_MAX_SPEED = 75f;
    public const float METEORITE_MEDIUM_MIN_SPEED = 75f;
    public const float METEORITE_MEDIUM_MAX_SPEED = 100f;
    public const float METEORITE_SMALL_MIN_SPEED = 100f;
    public const float METEORITE_SMALL_MAX_SPEED = 125f;

    public const float MIN_SPAWN_DELAY = 1f;
    public const float MAX_SPAWN_DELAY = 2f;

    private bool _gamePaused = false;
    private bool _debugMode = false;
    private bool _playerAlive = true;
    private int _points = 0;

    // Sets and Gets
    public void TogglePause(bool pause)
    { _gamePaused = pause; }
    public bool GamePaused()
    { return _gamePaused; }
    public void ToggleDebugMode(bool debugMode)
    { _debugMode = debugMode; }
    public bool InDebugMode()
    { return _debugMode; }
    public bool IsPlayerAlive()
    { return _playerAlive; }
    public void SetPlayerAlive(bool alive)
    { _playerAlive = alive; }
    public int GetPoints()
    { return _points; }
    public void ResetPoints()
    { 
        _points = 0;
        if (ScoreUpdatedInfo != null)
            ScoreUpdatedInfo(); // delegate event
    }
    public void AddPoints(int points_to_add)
    {
        _points += points_to_add;
        if (ScoreUpdatedInfo != null)
            ScoreUpdatedInfo(); // delegate event
    }

    public delegate void ScoreUpdated();
    public static event ScoreUpdated ScoreUpdatedInfo;

    private void Awake()
    {
        // Singleton initialization
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}