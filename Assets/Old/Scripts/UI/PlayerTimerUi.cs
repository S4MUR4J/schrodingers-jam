using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class PlayerTimerUi : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMeshPro;


        private void Start()
        {
            if (_textMeshPro == null)
            {
                Debug.LogError("TextMeshPro reference not set in PlayerTimerUI!");
                return;
            }


            _textMeshPro.text = "0.00s";

            GameManager.instance.OnPlayerTimerChanged += HandlePlayerTimerChanged;
        }

        private void HandlePlayerTimerChanged(object sender, GameManager.PlayerTimerEventArgs e)
        {
            var time = TimeSpan.FromSeconds(e.Value);
            _textMeshPro.text = $"{time.Minutes:D2}:{time.Seconds:D2}.{time.Milliseconds / 10:D2}";
        }
    }
}