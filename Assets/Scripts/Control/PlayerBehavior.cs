using com.ARTillery.Combat;
using com.ARTillery.Movement;
using System;
using UnityEngine;
using UnityEngine.AI;


namespace com.ARTillery.Control
{
    public class PlayerBehavior : MonoBehaviour
    {

        [SerializeField]
        private int _harvestValue = 10;

        [SerializeField]
        private int weaponPower = 10;

        [SerializeField]
        private float _harvestingTimer = 2f;

        private NavMeshAgent _agent;
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private LayerMask _interactableLayer;

        private Mover _mover;
        private Fighter _fighter;


        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _mover = GetComponent<Mover>();
            _fighter = GetComponent<Fighter>();
        }

        private void Update()
        {
            if (InteractWithCombat())
            {
                return;
            }
            if (InteractWithMovement())
            {
                return;
            }
            print("nothing to do");
        }

        private bool InteractWithCombat()
        {
            //highlight target 
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target is null)
                {
                    continue;
                }
                if (Input.GetMouseButton(1))
                {
                    _fighter.Attack(target);
                }
                return true;
            }
            return false;
        }


        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit && Input.GetMouseButton(1))
            {
                _fighter.CancelAttack();
                _mover.MoveTo(hit.point);
            }

            return hasHit;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
