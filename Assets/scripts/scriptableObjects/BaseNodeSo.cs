
using UnityEngine;

namespace scriptableObjects
{
    [CreateAssetMenu(fileName = "BaseLevelSO", menuName = "Scriptable Objects/BaseLevelSO")]
    public class BaseNodeSo : ScriptableObject
    {
        public GameObject prefab;
        public GameObject element;
    }
}

