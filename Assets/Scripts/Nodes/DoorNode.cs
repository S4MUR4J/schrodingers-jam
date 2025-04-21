using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nodes
{
    public class DoorNode : BaseNode
    {
        //TODO find better way to store the next scene (scriptable objects maybe?)
        [SerializeField] private string nextSceneName;

        public void WalkInto()
        {
            Debug.Log("Walk into scene " + nextSceneName);
          //  SceneManager.LoadScene(nextSceneName);
        }
    }
}