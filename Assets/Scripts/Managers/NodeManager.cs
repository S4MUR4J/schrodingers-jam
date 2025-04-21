using System.Collections.Generic;
using Nodes;
using UnityEngine;
using Utils;

namespace Managers
{
    public class NodeManager : MonoBehaviour
    {
        [SerializeField] private LayerMask nodeLayer;
        public static NodeManager Instance { get; private set; }
        
        private int _wordIndex;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        public void Register(BaseNode node)
        {
            if (_wordIndex >= Constants.Words.Count)
            {
                _wordIndex = 0;
            }
            
            node.Pattern = Constants.Words[_wordIndex++];
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
                if (!Physics.Raycast(origin, dir, out var hit, rayDistance)) continue;
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