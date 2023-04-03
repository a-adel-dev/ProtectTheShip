using com.ARTillery.Movement;
using System.Collections;
using UnityEngine;

namespace com.ARTillery.Combat
{
    public class Fighter : MonoBehaviour
    {

        [SerializeField]
        private float _range = 2f;

        [SerializeField]
        private float _attackRate = 0.5f;

        [SerializeField]
        private int _attackPower = 5;

        public float Range { get => _range; set => _range = value; }
        public float AttackRate { get => _attackRate; set => _attackRate = value; }
        public int AttackPower { get => _attackPower; set => _attackPower = value; }

        private void Update()
        {

        }

    }
}