using com.ARTillery.Control;
using com.Artillery.UI;
using com.ARTillery.Core;
using com.ARTillery.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace com.ARTillery.Combat
{
    public class CombatTarget : MonoBehaviour, ICursorTarget
    {
        [SerializeField]
        private Material _targetHighlightMaterial;
        [SerializeField]
        private StatCollection _statCollection;
        [SerializeField]
        private Renderer _renderer;

        [SerializeField]
        private HealthBar _healthBar;
        [FormerlySerializedAs("_floatingDamage")] [SerializeField]
        private FloatingText floatingText;

        private Points _health;
        private float _speed;
        private int _attackPower;
        private int _armor;
        private float _attackRate;

        public bool IsDead { get; private set; } = false;


        private Material _originalMaterial;

        private void Start()
        {
            if (_statCollection is null)
            {
                Debug.LogError($"Stat collection for {gameObject.name} is null, Please Add a stat collection!");
            }

            FillStats();
            _healthBar.SetMaxHealth(_health.MaxPoints);
            _healthBar.SetHealth(_health.MaxPoints);
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
            //update health bar
            _healthBar.SetHealth(_health.CurrentPoints);
            //display floating text
            floatingText.DisplayFloatingText(realDamage, FloatingTextType.Damage);

            if (_health.IsOutOfPoints())
            {
                //die
                IsDead = true;
            }
        }

        public bool IsCombatTargetDead()
        {
            return IsDead;
        }

        public void DestroyCombatTarget()
        {
            Destroy(gameObject);
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }

   
}