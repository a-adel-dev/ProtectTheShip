using com.ARTillery.Core;
using System.Collections;
using UnityEngine;

namespace com.ARTillery.Combat
{
    public class CombatTarget : MonoBehaviour
    {
        [SerializeField]
        private Material _targetHighlightMaterial;
        [SerializeField]
        private StatCollection _statCollection;
        [SerializeField]
        private Renderer _renderer;

        private Points _health;
        private float _speed;
        private int _attackPower;
        private int _armor;
        private float _attackRate;


        private Material _originalMaterial;

        private void Start()
        {
            if (_statCollection is null)
            {
                Debug.LogError($"Stat collection for {gameObject.name} is null, Please Add a stat collection!");
            }

            FillStats();
        }


        public void SetTargetVisual()
        {
            _originalMaterial = _renderer.material;
            _renderer.material = _targetHighlightMaterial;
        }

        public void ClearTargetVisual()
        {
            _renderer.material = _originalMaterial;
        }

        private void FillStats()
        {
            _health = new(_statCollection.Health);
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
                //die
            }
        }

    }

   
}