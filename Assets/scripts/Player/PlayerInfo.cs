using System.Collections.Generic;
using System.Linq;
using Logger = Utils.Logger;

namespace Player
{
    public class PlayerInfo : BaseElement 
    { 
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
            UpdateNeighbourNodes();
        }

        private void UpdateNeighbourNodes()
        {
            NeighbourNodes.Clear(); 
            NeighbourNodes.AddRange(GetNeighbourNodes());
        }
    
        private List<BaseNode> GetNeighbourNodes()
        {
            var neighbourNodesFiltered = NodeManager.instance.GetNeighbors(parentNode).Where(node => node.Pattern != null).ToList();
            neighbourNodesFiltered.ForEach(node => Logger.Log("Pattern to match: " + node.Pattern));
            
            return neighbourNodesFiltered;
        }
    }
}
