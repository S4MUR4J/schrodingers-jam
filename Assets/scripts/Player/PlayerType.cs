using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using Logger = Utils.Logger;

namespace Player
{
    public class PlayerType : MonoBehaviour
    {


        [SerializeField] private string _finishPattern = ":wqa!";

        [SerializeField] private PlayerInfo _playerInfo;

        private List<char> _typedLetters;

        public static PlayerType Instance { get; private set; }

        public string TypeSentence
        {
            get
            {
                var word = new string(_typedLetters.ToArray());
                return word;
            }
        }


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("PlayerInfo instance already exist!");
            }

            _typedLetters = new List<char>();
        }

        private void Update()
        {
            HandlePlayerInput();
            MatchInputWithPattern();
        }
        public event EventHandler<PlayerTypeEventArgs> OnPlayerTypeLetter;

        private void MatchInputWithPattern()
        {
            var matchingNodes = new List<BaseNode>();

            var nodes = _playerInfo.NeighbourNodes;
            nodes.ForEach(node =>
                {
                    if (node.pattern == null)
                        return;

                    var pattern = node.pattern + _finishPattern;
                    var typedSentence = TypeSentence;

                    var partRegex = new Regex("^" + Regex.Escape(pattern.Substring(0,
                        Math.Min(typedSentence.Length, pattern.Length))) + "$");
                    if (!partRegex.IsMatch(typedSentence))
                        return;

                    matchingNodes.Add(node);
                }
                );

            if (!matchingNodes.Any())
            {
                _typedLetters.Clear();
                OnPlayerTypeLetter?.Invoke(this, new PlayerTypeEventArgs(""));
            }


            var nodeThatFullRegexMatch = nodes.FirstOrDefault(node =>
            {
                var pattern = node.pattern + _finishPattern;
                var fullRegex = new Regex("^" + Regex.Escape(TypeSentence) + "$");
                return fullRegex.IsMatch(pattern);
            });
            if (nodeThatFullRegexMatch == null)
                return;

            if (nodeThatFullRegexMatch.HasElement())
            {
                _typedLetters.Clear();
                OnPlayerTypeLetter?.Invoke(this, new PlayerTypeEventArgs(""));
                return;
            }

            _typedLetters.Clear();
            _playerInfo.SetParentNode(nodeThatFullRegexMatch);
            _playerInfo.UpdateNeighbourNodes();
            OnPlayerTypeLetter?.Invoke(this, new PlayerTypeEventArgs(""));
        }

        private void HandlePlayerInput()
        {
            var lastPressedChar = GetPressedChar();
            if (lastPressedChar == null)
                return;

            _typedLetters.Add(lastPressedChar.Value);


            var args = new PlayerTypeEventArgs(TypeSentence);
            OnPlayerTypeLetter?.Invoke(this, args);


            Logger.Log("Last pressed key stoke in Player Type: " + lastPressedChar);
            Logger.Log("Current TypeSentence In Player Type: " + TypeSentence);
        }

        private static char? GetPressedChar()
        {
            var input = Input.inputString;

            if (!string.IsNullOrEmpty(input))
                return input[0];

            return null;
        }

        public class PlayerTypeEventArgs : EventArgs
        {

            public PlayerTypeEventArgs(string currentText)
            {
                CurrentText = currentText;
            }
            public string CurrentText { get; }
        }
    }
}
