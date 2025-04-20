using Managers;
using Nodes;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float lerpSpeed = 5f;
        public BaseNode PositionNode { get; private set; }
        public PlayerInput PlayerInput { get; set; }


        private bool _isLerping;
        private Vector3 _targetPosition;

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
            }
        }

        private void Update()
        {
            if (!_isLerping)
            {
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

        public void Move(BaseNode node)
        {
            Debug.Log($"Player Moving to Node {node}");

            node.DisableTextMesh();
            PositionNode.EnableTextMesh();

            PositionNode = node;

            transform.parent = node.GetTarget();
            _targetPosition = node.GetTarget().position;
            _isLerping = true;
        }
    }
}