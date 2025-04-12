using System.Collections.Generic;
using scriptableObjects;
using UnityEngine;

namespace levelsSO
{
    [CreateAssetMenu()]
    public class BaseLevelSo : ScriptableObject
    {
        public List<NodeRowSo> nodes = new();
    }
}

[System.Serializable]
public class NodeRowSo
{
    public List<BaseNodeSo> row = new ();
}