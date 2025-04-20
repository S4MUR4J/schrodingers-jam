using System.Collections.Generic;
using System.Linq;
using Nodes;
using UnityEngine;
using Utils;

namespace Managers
{
    public class NodeManager : MonoBehaviour
    {
        public static NodeManager Instance { get; private set; }

        private Dictionary<Vector2Int, BaseNode> nodeGrid = new();


        private int _wordIndex;

        private void Awake()
        {
            Instance = this;
        }

        public void Register(BaseNode node)
        {
            nodeGrid.TryAdd(node.GridPosition, node);
            if (_wordIndex >= nodeGrid.Count)
            {
                _wordIndex = 0;
            }

            node.Pattern = Constants.Words[_wordIndex++];
        }

        public BaseNode GetClosestNode(Vector3 position)
        {
            var gridPos = new Vector2Int(
                Mathf.RoundToInt(position.x),
                Mathf.RoundToInt(position.z)
            );

            return nodeGrid.GetValueOrDefault(gridPos);
        }


        public List<BaseNode> GetNeighbors(BaseNode node)
        {
            if (node == null)
            {
                return new List<BaseNode>();
            }

            List<BaseNode> neighbors = new();

            foreach (var dir in Constants.Directions)
            {
                var neighborPos = node.GridPosition + dir;
                if (nodeGrid.TryGetValue(neighborPos, out var neighbor))
                {
                    neighbors.Add(neighbor);
                }
            }

            Debug.Log(string.Join(", ", neighbors.Select(n => n.Pattern)));
            
            return neighbors;
        }
    }
}