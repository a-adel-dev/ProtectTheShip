using com.ARTillery.Movement;
using System.Collections;
using UnityEngine;

namespace com.ARTillery.Combat
{
    public class Fighter : MonoBehaviour
    {

        [SerializeField]
        private float range = 2f;

        private CombatTarget _target;

        public float Range { get => range; set => range = value; }

        private void Update()
        {
            if (_target == null) return;
            if (GetIsOutOfRange())
            {
                GetComponent<Mover>().MoveTo(_target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Stop();
            }
        }

        private bool GetIsOutOfRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) >= Range;
        }

        public void Attack(CombatTarget target)
        {
            _target = target;
            Debug.Log($"Attacking {target.name}");
        }

        public void CancelAttack()
        {
            _target = null;
        }
    }
}