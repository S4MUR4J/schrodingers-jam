using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        private void Awake()
        {
            GameManager.instance.Player = gameObject;
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
                mainCamera.fieldOfView = 90f;
                mainCamera.transform.localPosition = new Vector3(0, 3, 0);
                mainCamera.transform.localRotation = Quaternion.Euler(90, 0, 0);
            }


            NeighbourNodes = new List<BaseNode>();
            UpdateNeighbourNodes();
        }

        public void UpdateNeighbourNodes()
        {
            NeighbourNodes = new List<BaseNode>();
            NeighbourNodes.AddRange(GetNeighbourNodes());
        }

        private List<BaseNode> GetNeighbourNodes()
        {
            if (parentNode == null)
            {
                Debug.LogError("Player's parentNode is null!");
                return new List<BaseNode>();
            }

            var neighbourNodesFiltered = NodeManager.instance.GetNeighbors(parentNode)
                .Where(node => node.pattern != null).ToList();
            // neighbourNodesFiltered.ForEach(node => Logger.Log("Pattern to match: " + node.pattern));

            return neighbourNodesFiltered;
        }

        public override void DestroySelf()
        {
            var mainCamera = Camera.main;

            if (mainCamera == null)
            {
                Debug.LogError("No camera found in player!");
            }
            else
            {
                mainCamera.transform.SetParent(null);
            }

            Destroy(gameObject);
        }
    }
}
