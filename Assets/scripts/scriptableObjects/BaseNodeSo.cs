
using UnityEngine;

namespace scriptableObjects
{
    [CreateAssetMenu()]
    public class BaseNodeSo : ScriptableObject
    {
        public GameObject prefab;
        public BaseElementSo elementSo;
    }
}

