using System.Collections.Generic;
using Managers;
using Nodes;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float lerpSpeed = 5f;
        private BaseNode PositionNode { get; set; }
        public PlayerTypingInput PlayerInput { get; set; }


        private bool _isLerping;
        private Vector3 _targetPosition;


        private List<BaseNode> _neighbours;

        private void Awake()
        {
            GameManager.Instance.Player = this;

            PositionNode = NodeManager.Instance.GetClosestNode(transform.position);

            if (PositionNode == null)
            {
                Debug.LogError("Starting Node Not Found For Player");
            }
            else
            {
                PositionNode.DisableTextMesh();
                _neighbours = NodeManager.Instance.GetNeighbors(PositionNode);
                HighlightNeighbours(true);
            }


            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            if (!_isLerping)
            {
                if (PositionNode is DoorNode doorNode)
                {
                    doorNode.WalkInto();
                }
                return;
            }

            transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * lerpSpeed);

            if (!(Vector3.Distance(transform.position, _targetPosition) < 0.001f))
            {
                return;
            }

            transform.position = _targetPosition;
            _isLerping = false;
        }

        public List<BaseNode> GetNeighbours()
        {
            return _neighbours;
        }

        public void Move(BaseNode node)
        {
            PositionNode.EnableTextMesh();
            HighlightNeighbours(false);


            node.DisableTextMesh();
            _neighbours = NodeManager.Instance.GetNeighbors(node);
            HighlightNeighbours(true);
            PositionNode = node;


            //move player to target position
            transform.parent = node.GetTarget();
            _targetPosition = node.GetTarget().position;
            _isLerping = true;
        }

        private void HighlightNeighbours(bool highlight)
        {
            foreach (var neighbour in _neighbours)
            {
                neighbour.Highlight(highlight);
            }
        }
    }
}