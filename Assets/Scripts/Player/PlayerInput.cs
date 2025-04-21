using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Managers;
using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        public event EventHandler<PlayerTypeEventArgs> OnPlayerTypeLetter;

        [SerializeField] private string _finishPattern = ".";
        private Player _player;

        private readonly List<char> _typedLetters = new();
        private string TypedSentence => new(_typedLetters.Where(c => !char.IsControl(c)).ToArray());

        public class PlayerTypeEventArgs : EventArgs
        {
            public string CurrentText { get; }

            public PlayerTypeEventArgs(string currentText)
            {
                CurrentText = currentText;
            }
        }

        private void Awake()
        {
            _player = GameManager.Instance.Player;

            if (_player == null)
            {
                Debug.LogError("Player is null in PlayerInput");
                return;
            }

            _player.PlayerInput = this;


            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            HandleInput();
            TryMatchPattern();
        }

        private void HandleInput()
        {
            // Optional reset key
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                _typedLetters.Clear();
                RaiseTypeEvent();
                return;
            }

            var pressedChar = GetPressedChar();
            if (pressedChar == null) return;

            _typedLetters.Add(pressedChar.Value);
            RaiseTypeEvent();
        }

        private void TryMatchPattern()
        {
            var neighbors = _player.GetNeighbours();

            if (neighbors == null || neighbors.Count == 0)
            {
                return;
            }

            var typed = TypedSentence;
            // Find partial matches
            var matchingNodes = neighbors
                .Where(n => !string.IsNullOrEmpty(n.Pattern))
                .Where(n =>
                {
                    var expected = n.Pattern + _finishPattern;
                    var partialPattern = expected[..Math.Min(typed.Length, expected.Length)];
                    return expected.StartsWith(typed);
                })
                .ToList();

            if (!matchingNodes.Any())
            {
                _typedLetters.Clear();
                RaiseTypeEvent();
                return;
            }


            // Check for exact match
            var matchedNode = matchingNodes.FirstOrDefault(n =>
            {
                //  if (!typed.EndsWith(_finishPattern))
                //       return false;

                //       return typed == n.Pattern + _finishPattern;
                return typed == n.Pattern;
            });

            if (!matchedNode)
            {
                return;
            }


            _typedLetters.Clear();
            RaiseTypeEvent();
            _player.Move(matchedNode);
        }

        private void RaiseTypeEvent()
        {
            OnPlayerTypeLetter?.Invoke(this, new PlayerTypeEventArgs(TypedSentence));
        }

        private static char? GetPressedChar()
        {
            var input = Input.inputString;
            return string.IsNullOrEmpty(input) ? null : input[0];
        }
    }
}