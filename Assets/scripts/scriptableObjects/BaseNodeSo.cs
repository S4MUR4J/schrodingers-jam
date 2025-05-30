using UnityEngine;

namespace scriptableObjects
{
    [CreateAssetMenu]
    public class BaseNodeSo : ScriptableObject
    {
        public GameObject prefab;
        public GameObject elementPrefab;
        public bool withPattern;
    }
}
