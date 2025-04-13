using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerTypeUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMeshPro;


        private void Awake()
        {
            if (_textMeshPro == null)
            {
                Debug.LogError("TextMeshPro reference not set in PlayerTypeUI!");
                return;
            }


            _textMeshPro.text = string.Empty;
            GameManager.instance.OnLevelLoad += HandleLevelLoad;
        }


        private void HandlePlayerTyped(object sender, PlayerType.PlayerTypeEventArgs e)
        {
            _textMeshPro.text = e.CurrentText;
        }

        private void HandleLevelLoad(object sender, EventArgs e)
        {
            var playerType = GameManager.instance.Player.GetComponent<PlayerType>();
            if (playerType == null)
            {
                Debug.LogError("PlayerType component not found on Player!");
                return;
            }

            playerType.OnPlayerTypeLetter += HandlePlayerTyped;
        }
    }
}