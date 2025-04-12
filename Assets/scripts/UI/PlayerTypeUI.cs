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
            _textMeshPro.text = string.Empty;
        }

        private void Start()
        {
            if (_textMeshPro == null)
            {
                Debug.LogError("TextMeshPro reference not set in PlayerTypeUI!");
                return;
            }

            PlayerType.instance.OnPlayerTypeLetter += HandlePlayerTyped;
        }


        private void HandlePlayerTyped(object sender, PlayerType.PlayerTypeEventArgs e)
        {
            Debug.Log("HANDLE CALLED! to set text: " + e.CurrentText);
            _textMeshPro.text = e.CurrentText;
        }
    }
}
