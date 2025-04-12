using System.Collections.Generic;
using scriptableObjects;
using UnityEngine;

namespace levelsSO
{
    [CreateAssetMenu(fileName = "BaseLevelSO", menuName = "Scriptable Objects/BaseLevelSO")]
    public class BaseLevelSo : ScriptableObject
    {
        public List<NodeRow> nodes = new();
    }
}

[System.Serializable]
public class NodeRow
{
    public List<BaseNodeSo> row = new ();
}