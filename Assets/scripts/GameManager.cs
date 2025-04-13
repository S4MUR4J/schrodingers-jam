using levelsSO;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
        NodeManager.instance.LoadLevel(startLevel);
    }

    public void LoadLevel(BaseLevelSo level)
    {
        NodeManager.instance.ClearLevel();
        NodeManager.instance.LoadLevel(level);
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