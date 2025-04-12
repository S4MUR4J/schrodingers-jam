using System;
using System.Collections.Generic;
using UnityEngine;

namespace scriptableObjects
{
    [CreateAssetMenu()]
    public class BaseElementSo : ScriptableObject
    {
        public GameObject prefab;
        public List<String> patterns;
        
    }
}
