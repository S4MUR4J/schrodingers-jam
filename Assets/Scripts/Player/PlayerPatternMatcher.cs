using System.Linq;
using UnityEngine;

namespace Player
{
    public class PlayerPatternMatcher : MonoBehaviour
    {
        [SerializeField] private PlayerTypingInput typingInput;
        [SerializeField] private Player player;

        private void Awake()
        {
            if (player == null)
            {
                Debug.LogError("Player is missing in PlayerPatternMatche");
                return;
            }

            if (typingInput == null)
            {
                Debug.LogError("PlayerTypingInput component missing!");
            }

            DontDestroyOnLoad(this);
        }

        public void TryMatch()
        {
            var input = typingInput.GetCurrentInput();
            var neighbors = player.GetNeighbours();

            if (neighbors == null || neighbors.Count == 0)
                return;

            var matchingNodes = neighbors
                .Where(n => !string.IsNullOrEmpty(n.Pattern))
                .Where(n =>
                {
                    var expected = n.Pattern;
                    return expected.StartsWith(input);
                })
                .ToList();

            if (!matchingNodes.Any())
            {
                typingInput.Clear();
                return;
            }

            var matchedNode = matchingNodes.FirstOrDefault(n => input == n.Pattern);
            if (matchedNode == null)
                return;

            typingInput.Clear();
            player.Move(matchedNode);
        }
    }
}