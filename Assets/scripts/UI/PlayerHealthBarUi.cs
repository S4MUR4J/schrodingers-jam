using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerHealthBarUi : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMeshPro;


        private void Start()
        {
            if (_textMeshPro == null)
            {
                Debug.LogError("TextMeshPro reference not set in PlayerTypeUI!");
                return;
            }


            _textMeshPro.text = "5/5";

            GameManager.instance.OnPlayerHealthChanged += HandlePlayerHealthChanged;
        }

        private void HandlePlayerHealthChanged(object sender, GameManager.PlayerHealthEventArgs e)
        {
            _textMeshPro.text = e.Value + "/5";
        }
    }
}