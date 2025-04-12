using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Logger = Utils.Logger;

namespace Player
{
    public class PlayerInfo : BaseElement
    {
        private List<BaseNode> _neighbourNodes;

        public List<BaseNode> NeighbourNodes
        {
            get { return _neighbourNodes; }
            private set { _neighbourNodes = value; }
        }

        private void Start()
        {
            //set camera as child of player and add offset
            var mainCamera = Camera.main;

            if (mainCamera == null)
            {
                Debug.LogError("No camera found in player!");
            }
            else
            {
                mainCamera.transform.SetParent(transform);
                mainCamera.transform.localPosition = new Vector3(0, 5, -5); 
                mainCamera.transform.localRotation = Quaternion.Euler(45, 0, 0); 
            }


            NeighbourNodes = new List<BaseNode>();
            UpdateNeighbourNodes();
        }

        public void UpdateNeighbourNodes()
        {
            NeighbourNodes.Clear();
            NeighbourNodes.AddRange(GetNeighbourNodes());
        }

        private List<BaseNode> GetNeighbourNodes()
        {
            var neighbourNodesFiltered = NodeManager.instance.GetNeighbors(parentNode)
                .Where(node => node.GetElement()?.Pattern != null).ToList();
            neighbourNodesFiltered.ForEach(node => Logger.Log("Pattern to match: " + node.GetElement().Pattern));

            return neighbourNodesFiltered;
        }
    }
}