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

            _controls.Gameplay.TypeCharacter.performed += OnAnyKeyPressed;
            _controls.Gameplay.ClearInput.performed += OnClearInput;
            _controls.Gameplay.ConfirmInput.performed += OnConfirmInput;
        }

        private void OnDisable()
        {
            _controls.Gameplay.TypeCharacter.performed -= OnAnyKeyPressed;
            _controls.Gameplay.ClearInput.performed -= OnClearInput;
            _controls.Gameplay.ConfirmInput.performed -= OnConfirmInput;
            _controls.Gameplay.Disable();
        }

        private void OnClearInput(InputAction.CallbackContext ctx)
        {
            Clear();
        }

        private void OnConfirmInput(InputAction.CallbackContext ctx)
        {
            Debug.Log("OnConfirmInput called with current input" + GetCurrentInput());
            patternMatcher?.TryMatch();
        }

        private void OnAnyKeyPressed(InputAction.CallbackContext ctx)
        {
            var keyboard = Keyboard.current;

            var pressedKey = keyboard?.allKeys.FirstOrDefault(k => k is { wasPressedThisFrame: true });

            if (pressedKey == null || string.IsNullOrEmpty(pressedKey.displayName))
                return;

            if (pressedKey == keyboard.enterKey ||
                pressedKey == keyboard.tabKey ||
                pressedKey == keyboard.escapeKey ||
                pressedKey == keyboard.shiftKey ||
                pressedKey == keyboard.ctrlKey ||
                pressedKey == keyboard.altKey)
                return;

            var inputChar = pressedKey.displayName[0];

            if (!char.IsLetterOrDigit(inputChar) && !char.IsPunctuation(inputChar) && !char.IsSymbol(inputChar))
                return;

            inputChar = keyboard.shiftKey.isPressed ? char.ToUpper(inputChar) : char.ToLower(inputChar);

            _typedLetters.Add(inputChar);
            OnTypedChanged?.Invoke(TypedSentence);
        }

        public void Clear()
        {
            _typedLetters.Clear();
            OnTypedChanged?.Invoke(TypedSentence);
        }
    }
}