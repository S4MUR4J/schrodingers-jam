using UnityEngine;

namespace Menu
{
    public class MenuHider : MonoBehaviour
    {
        [SerializeField] private GameObject _canvas;
        private bool paused;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape))
                return;

            PauseFunc();
        }

        private void PauseFunc()
        {
            paused = !paused;

            Time.timeScale = paused ? 0f : 1f;
            _canvas.SetActive(paused);
        }
    }
}
