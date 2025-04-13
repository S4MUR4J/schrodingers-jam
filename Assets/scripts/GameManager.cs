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
    }

    private void Start()
    {
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