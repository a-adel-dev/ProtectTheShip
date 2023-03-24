using com.ARTillery.Combat;
using UnityEngine;
using UnityEngine.AI;

namespace com.ARTillery.Control
{
    public class PlayerCombatState : PlayerBaseState
    {
        private PlayerBehavior _player;
        private Fighter _fighter;
        public PlayerCombatState(PlayerBehavior player)
        {
            _player = player;
            
        }

        public override void EnterState()
        {
            _player.SetCurrentState(this);
            Debug.Log("Entering Combat State");
            if (_fighter is null)
            {
                _fighter = _player.Fighter;
            }
            
        }

        public override void UpdateState()
        {




            if (Input.GetMouseButton(1))
            {
                RaycastHit[] hits = Physics.RaycastAll(_player.GetMouseRay());
                if (hits.Length == 0)
                {
                    Debug.Log("nothing to do");
                    return;
                }
                foreach (RaycastHit hit in hits)
                {
                    if (IsCombatTarget(hit))
                    {
                        SetCombatTarget(hit);
                    }

                    else
                    {
                        _player.ClearCombatTarget();

                        if (HasPath(hit))
                        {
                            _player.MoveState.SetDestination(hit.point);
                            ExitState();
                            _player.MoveState.EnterState();
                        }
                    }
                }
            }
        }

        public override void ExitState()
        {

        }

        private CombatTarget IsCombatTarget(RaycastHit hit)
        {
            return hit.transform.GetComponent<CombatTarget>();
        }

        private void SetCombatTarget(RaycastHit hit)
        {
            if (_player.GetCombatTarget() == hit.transform.GetComponent<CombatTarget>())
            {
                return;
            }
            else
            {
                _player.SetCombatTarget( hit.transform.GetComponent<CombatTarget>());
                MoveToCombatTarget();
            }
        }

        private void MoveToCombatTarget()
        {
            ExitState();
            _player.MoveState.SetDestination(_player.GetCombatTarget().transform.position);
            _player.MoveState.EnterState();
        }

        private bool HasPath(RaycastHit hit)

        {
            var navMeshPath = new NavMeshPath();
            return _player.Agent.CalculatePath(hit.point, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete;
        }

        public override string ToString()
        {
            return "Combat";
        }
    }
}