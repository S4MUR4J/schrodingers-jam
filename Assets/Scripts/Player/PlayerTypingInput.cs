using Managers;

namespace Player
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;


    public class PlayerTypingInput : MonoBehaviour
    {
        public event Action<string> OnTypedChanged;

        [SerializeField] private KeyCode clearKey = KeyCode.Backspace;
        [SerializeField] private KeyCode confirmKey = KeyCode.Return;
        [SerializeField] private PlayerPatternMatcher patternMatcher;

        private readonly List<char> _typedLetters = new();
        private string TypedSentence => new(_typedLetters.Where(c => !char.IsControl(c)).ToArray());

        public string GetCurrentInput() => TypedSentence;

        private void Awake()
        {
            if (patternMatcher == null)
            {
                Debug.LogError("Pattern Matcher is null.");
                return;
            }

            GameManager.Instance.Player.PlayerInput = this;
            
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            HandleTyping();
            HandleConfirm();
        }

        private void HandleTyping()
        {
            if (Input.GetKeyDown(clearKey))
            {
                Clear();
                return;
            }

            var pressedChar = Input.inputString.FirstOrDefault();
            if (pressedChar == '\0') return;

            _typedLetters.Add(pressedChar);
            OnTypedChanged?.Invoke(TypedSentence);
        }

        private void HandleConfirm()
        {
            if (Input.GetKeyDown(confirmKey))
            {
                patternMatcher?.TryMatch();
            }
        }

        public void Clear()
        {
            _typedLetters.Clear();
            OnTypedChanged?.Invoke(TypedSentence);
        }
    }
}