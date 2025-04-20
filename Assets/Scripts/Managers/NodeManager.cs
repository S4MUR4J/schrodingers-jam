using System.Collections.Generic;
using System.Linq;
using Nodes;
using UnityEngine;
using Utils;

namespace Managers
{
    public class NodeManager : MonoBehaviour
    {
        [SerializeField] private LayerMask nodeLayer;
        public static NodeManager Instance { get; private set; }

        private readonly List<BaseNode> allNodes = new();


        private int _wordIndex;

        private void Awake()
        {
            Instance = this;
        }

        public void Register(BaseNode node)
        {
            allNodes.Add(node);
            if (_wordIndex >= Constants.Words.Count)
            {
                _wordIndex = 0;
            }

            node.Pattern = Constants.Words[_wordIndex++];
        }

        public BaseNode GetClosestNode(Vector3 position)
        {
            if (allNodes.Count == 0) return null;

            BaseNode closest = null;
            var closestDist = float.MaxValue;

            foreach (var node in allNodes)
            {
                var dist = Vector3.Distance(node.transform.position, position);
                if (!(dist < closestDist)) continue;
                closest = node;
                closestDist = dist;
            }

            return closest;
        }


        public List<BaseNode> GetNeighbors(BaseNode node)
        {
            if (!node) return new List<BaseNode>();

            var results = new List<BaseNode>();
            var directions = new Vector3[]
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

            Debug.DrawRay(origin, Vector3.forward * rayDistance, Color.red, 1f);
            Debug.DrawRay(origin, Vector3.back * rayDistance, Color.red, 1f);
            Debug.DrawRay(origin, Vector3.left * rayDistance, Color.red, 1f);
            Debug.DrawRay(origin, Vector3.right * rayDistance, Color.red, 1f);

            Debug.Log($"[Raycast] Neighbors of {node.Pattern}: {string.Join(", ", results.Select(n => n.Pattern))}");
            return results;
        }
    }
}