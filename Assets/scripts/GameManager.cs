using System;
using levelsSO;
using Player;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public event EventHandler OnLevelLoad;

    public GameObject Player { get; set; }

    [SerializeField] private BaseLevelSo startLevel;


    public event EventHandler<PlayerHealthEventArgs> OnPlayerHealthChanged;

    public class PlayerHealthEventArgs : EventArgs
    {
        public PlayerHealthEventArgs(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }


    [SerializeField] private int maxHealth = 5;

    private int _currentHealth;

    public int CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            OnPlayerHealthChanged?.Invoke(this, new PlayerHealthEventArgs(value));

            Debug.LogWarning("Health changed to: " + value);

            if (_currentHealth <= 0)
            {
                QuitGame();
            }
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        CurrentHealth = maxHealth;
        Debug.LogWarning("Starting health: " + CurrentHealth);
    }

    private void Start()
    {
        if (!Application.isPlaying) return;
        LoadLevel(startLevel);
    }


    public void LoadLevel(BaseLevelSo level)
    {
        NodeManager.instance.ClearLevel();
        NodeManager.instance.LoadLevel(level);
        Player.GetComponent<PlayerInfo>().UpdateNeighbourNodes();
        OnLevelLoad?.Invoke(this, EventArgs.Empty);
    }


    public void QuitGame()
    {
#if UNITY_EDITOR
        // If running in the Unity Editor, stop the play mode
        EditorApplication.isPlaying = false;
#else
        // If in a build, quit the application
        Application.Quit();
#endif
    }
}