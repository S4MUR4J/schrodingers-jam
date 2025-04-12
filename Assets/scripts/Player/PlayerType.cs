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
        [SerializeField]
        private string _finishPattern = ":wqa";

        [SerializeField]
        private PlayerInfo _playerInfo;

        private string TypeSentence
        {
            get
            {
                var word = new string(_typedLetters.ToArray());
                return word;
            }
        }

        private List<char> _typedLetters;


        private void Awake()
        {
            _typedLetters = new List<char>();
        }

        private void Update()
        {
            HandlePlayerInput();
            MatchInputWithPattern();
        }

        private void MatchInputWithPattern()
        {
            var matchingNodes = new List<BaseNode>();

            var nodes = _playerInfo.NeighbourNodes;
            nodes.ForEach(node =>
                {
                    var pattern = (node.Pattern + _finishPattern).ToUpper();
                    var typedSentence = TypeSentence;

                    var partRegex = new Regex("^" + Regex.Escape(pattern.Substring(startIndex: 0, length: typedSentence.Length)) + "$");
                    if (!partRegex.IsMatch(typedSentence))
                        return;

                    matchingNodes.Add(node);
                }
                );

            if (!matchingNodes.Any())
                _typedLetters.Clear();


            var fullRegexMatch = nodes.FirstOrDefault(node =>
            {
                var pattern = (node.Pattern + _finishPattern).ToUpper();
                var fullRegex = new Regex("^" + Regex.Escape(TypeSentence) + "$");
                return fullRegex.IsMatch(pattern);
            });
            if (fullRegexMatch == null)
                return;

            // TODO przeniesc w managerze
            throw new NotImplementedException();
        }

        private void HandlePlayerInput()
        {
            var lastPressedChar = GetPressedChar();
            if (lastPressedChar == null)
                return;

            _typedLetters.Add(lastPressedChar.Value);
            Logger.Log("Last pressed key stoke is: " + lastPressedChar);
        }

        private static char? GetPressedChar()
        {
            var codeValues = Enum.GetValues(typeof(KeyCode));
            foreach (KeyCode keyCode in codeValues)
            {
                if (!Input.GetKeyDown(keyCode))
                    continue;

                return keyCode.ToString()[0];
            }

            return null;
        }
    }
}
