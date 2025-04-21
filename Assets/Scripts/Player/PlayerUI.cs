using Managers;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMeshPro;

        private void Start()
        {
            if (_textMeshPro == null)
            {
                Debug.LogError("TextMeshPro reference not set in PlayerTypeUI!");
                return;
            }

            _textMeshPro.text = "";


            GameManager.Instance.Player.PlayerInput.OnPlayerTypeLetter += HandlePlayerHealthChanged;

            DontDestroyOnLoad(this);
        }

        private void HandlePlayerHealthChanged(object sender, PlayerInput.PlayerTypeEventArgs e)
        {
            _textMeshPro.text = e.CurrentText;
        }
    }
}