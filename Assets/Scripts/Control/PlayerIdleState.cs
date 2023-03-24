using com.ARTillery.Combat;
using UnityEngine;
using UnityEngine.AI;

namespace com.ARTillery.Control
{
    public class PlayerIdleState : PlayerBaseState
    {
        private PlayerBehavior _player;

        public PlayerIdleState(PlayerBehavior player)
        {
            _player = player;
        }

        public override void EnterState()
        {
            _player.SetCurrentState(this);
            Debug.Log("Entering Idle State");
            _player.Mover.Stop();

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
            if (_player.Target is not null)
            {
                MoveToCombatTarget();
            }
        }

        private bool HasPath(RaycastHit hit)

        {
            var navMeshPath = new NavMeshPath();
            return _player.Agent.CalculatePath(hit.point, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete;
        }



        private void SetCombatTarget(RaycastHit hit)
        {
            _player.Target = hit.transform.GetComponent<CombatTarget>();
        }

        private static CombatTarget IsCombatTarget(RaycastHit hit)
        {
            return hit.transform.GetComponent<CombatTarget>();
        }

        private void MoveToCombatTarget()
        {
            ExitState();
            _player.MoveState.SetDestination(_player.Target.transform.position);
            _player.MoveState.EnterState();
        }

        public override void ExitState()
        {
            Debug.Log("leaving idle state");
        }

        public override string ToString()
        {
            return "Idle";
        }
    }
}