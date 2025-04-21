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
            var matchedNode = player.GetNeighbours()
                ?.Where(n => !string.IsNullOrEmpty(n.Pattern) && n.Pattern.StartsWith(input))
                .FirstOrDefault(n => n.Pattern == input);

            typingInput.Clear();

            if (matchedNode)
            {
                player.Move(matchedNode);
            }
        }
    }
}