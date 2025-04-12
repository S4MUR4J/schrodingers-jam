using Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerTypeUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _textMeshPro;

        [SerializeField]
        private PlayerType _playerType;

        private void Start()
        {
            _textMeshPro.text = string.Empty;
        }

        private void Update()
        {
            _textMeshPro.text = _playerType.TypeSentence;
        }
    }
}
