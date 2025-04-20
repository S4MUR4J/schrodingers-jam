using System.Collections.Generic;
using levelsSO;
using UnityEngine;

namespace scriptableObjects
{
    [CreateAssetMenu(fileName = "LevelDatabase", menuName = "Game/Level Database")]
    public class LevelDatabase : ScriptableObject
    {
        public List<BaseLevelSo> allLevels;
    }
}