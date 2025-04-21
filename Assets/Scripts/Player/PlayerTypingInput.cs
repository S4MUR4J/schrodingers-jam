using Managers;
using UnityEngine.InputSystem;

namespace Player
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;


    public class PlayerTypingInput : MonoBehaviour
    {
        public event Action<string> OnTypedChanged;

        [SerializeField] private PlayerPatternMatcher patternMatcher;

        private readonly List<char> _typedLetters = new();
        private string TypedSentence => new(_typedLetters.Where(c => !char.IsControl(c)).ToArray());

        private PlayerControls _controls;

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

        private void OnEnable()
        {
            _controls = new PlayerControls();
            _controls.Gameplay.Enable();

            Keyboard.current.onTextInput += OnTextInput;


            _controls.Gameplay.ClearInput.performed += OnClearInput;
            _controls.Gameplay.ConfirmInput.performed += OnConfirmInput;
        }

        private void OnDisable()
        {
            Keyboard.current.onTextInput -= OnTextInput;


            _controls.Gameplay.ClearInput.performed -= OnClearInput;
            _controls.Gameplay.ConfirmInput.performed -= OnConfirmInput;
            _controls.Gameplay.Disable();
        }

        private void OnTextInput(char character)
        {
            if (char.IsControl(character)) return;

            _typedLetters.Add(character);
            OnTypedChanged?.Invoke(TypedSentence);
        }

        private void OnClearInput(InputAction.CallbackContext ctx)
        {
            Clear();
        }

        private void OnConfirmInput(InputAction.CallbackContext ctx)
        {
            patternMatcher?.TryMatch();
        }

        public void Clear()
        {
            _typedLetters.Clear();
            OnTypedChanged?.Invoke(TypedSentence);
        }
    }
}