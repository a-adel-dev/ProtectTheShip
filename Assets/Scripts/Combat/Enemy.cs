using com.ARTillery.Core;
using UnityEngine;

namespace com.ARTillery.Combat
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private StatCollection _statCollection;

        private Points _health;
        private float _speed;
        private int _attackPower;
        private int _armor;
        private float _attackRate;

        private bool _isDead = false;

        private void Start()
        {
            if (_statCollection is null)
            {
                Debug.LogError($"Stat collection for {gameObject.name} is null, Please Add a stat collection!");
            }

            FillStats();
        }

        private void FillStats()
        {
            _health = new( _statCollection.Health);
            _speed = _statCollection.Speed;
            _attackPower = _statCollection.AttackPower;
            _armor = _statCollection.Armor;
            _attackRate = _statCollection.AttackRate;
        }

        public void TakeDamage(int damageValue)
        {
            int realDamage = damageValue - _armor;
            _health.ReducePoints(realDamage);
            if (_health.IsOutOfPoints())
            {
                _isDead = true;
            }
        }
    }
}