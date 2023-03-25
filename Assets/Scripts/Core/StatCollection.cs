
using UnityEngine;

namespace com.ARTillery.Core
{
    [CreateAssetMenu(fileName = "Stat Collection", menuName = "ScriptableObjects/Stat Collections", order = 1)]
    public class StatCollection : ScriptableObject
    {
        public int Health;
        public float Speed;
        public int AttackPower;
        public int Armor;
        public float AttackRate;
        
    }
}