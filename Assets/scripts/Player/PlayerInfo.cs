using System;
using System.Collections.Generic;
using System.Linq;
using Logger = Utils.Logger;

namespace Player
{
    public class PlayerInfo : BaseElement 
    { 
        private Tuple<int, int> _currentPosition;
        private List<BaseNode> _neighbourNodes;

        public List<BaseNode> NeighbourNodes
        {
            get
            {
                return _neighbourNodes;
            }
            private set
            {
                _neighbourNodes = value;
            }
        }

        private void Start()
        {
            NeighbourNodes = new List<BaseNode>();
            _currentPosition = parentNode.Position;
            UpdateNeighbourNodes();
        }

        private void UpdateNeighbourNodes()
        {
            NeighbourNodes.Clear(); 
            NeighbourNodes.AddRange(GetNeighbourNodes());
        }
    
        private List<BaseNode> GetNeighbourNodes()
        {
            var neighbourNodesFiltered = NodeManager.instance.GetNeighbors(parentNode);
            neighbourNodesFiltered.ForEach(node => Logger.Log("Pattern to match: " + node.Pattern));
            
            return neighbourNodesFiltered;
        }

        private List<Tuple<int, int>> GetNeighbourNodesPosition()
        {
            var neighbourNodes = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(_currentPosition.Item1 + 1, _currentPosition.Item2), // UP
                new Tuple<int, int>(_currentPosition.Item1 - 1, _currentPosition.Item2), // DOWN 
                new Tuple<int, int>(_currentPosition.Item1, _currentPosition.Item2 + 1), // RIGHT
                new Tuple<int, int>(_currentPosition.Item1, _currentPosition.Item2 - 1), // LEFT
            };
            return neighbourNodes;
        }
    }
}
