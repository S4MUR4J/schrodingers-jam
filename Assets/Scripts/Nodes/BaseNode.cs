using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;

namespace Nodes
{
    public class BaseNode : MonoBehaviour
    {
        public string Pattern
        {
            get => textMesh.text;
            set => textMesh.text = value;
        }


        [Header("Design")] [SerializeField] private bool canMove;
        [SerializeField] private Color baseTextColor;
        [SerializeField] private Color highlightedTextColor;

        [Header("Unity Setup")] [SerializeField]
        private TextMeshProUGUI textMesh;

        [SerializeField] private Transform target;


        private void Awake()
        {
            if (!canMove)
            {
                return;
            }

            if (target == null)
            {
                Debug.LogError("Target is null");
                return;
            }

            if (textMesh == null)
            {
                Debug.LogError("TextMesh is null");
                return;
            }

            textMesh.color = baseTextColor;

            NodeManager.Instance.Register(this);
        }

        public void Highlight(bool highlighted)
        {
            Debug.Log($"Highlighted called with: {highlighted}");

            if (!textMesh)
            {
                return;
            }

            if (highlighted)
            {
                textMesh.color = highlightedTextColor;
                return;
            }

            textMesh.color = baseTextColor;
        }

        public void EnableTextMesh()
        {
            if (textMesh)
            {
                textMesh.enabled = true;
            }
        }

        public void DisableTextMesh()
        {
            if (textMesh)
            {
                textMesh.enabled = false;
            }
        }

        public bool CanMove()
        {
            return canMove;
        }

        public Transform GetTarget()
        {
            return target;
        }


        public List<BaseNode> GetNeighbors(BaseNode node)
        {
            if (!node) return new List<BaseNode>();

            var results = new List<BaseNode>();
            var directions = new[]
            {
                Vector3.forward, // Z+
                Vector3.back, // Z-
                Vector3.right, // X+
                Vector3.left // X-
            };

            const float rayDistance = 2; // how far to look for neighbors
            var origin = node.transform.position + Vector3.up * 0.5f; // lift the ray slightly

            foreach (var dir in directions)
            {
                if (!Physics.Raycast(origin, dir, out RaycastHit hit, rayDistance)) continue;
                var other = hit.collider.GetComponent<BaseNode>();
                if (other != null && other != node && other.CanMove())
                {
                    results.Add(other);
                }
            }

            return results;
        }
    }
}