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
                    if (hit.transform.GetComponent<CombatTarget>())
                    {
                        _player.Target = hit.transform.GetComponent<CombatTarget>();
                    }

                    else
                    {
                        _player.Target = null;
                        //create empty path
                        NavMeshPath navMeshPath = new NavMeshPath();
                        //create path and check if it can be done
                        // and check if navMeshAgent can reach its target
                        if (_player.Agent.CalculatePath(hit.point, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
                        {
                            //move to target
                            _player.MoveState.SetDestination(hit.point);
                            ExitState();
                            _player.MoveState.EnterState();
                        }
                    }
                }
            }

            if (_player.Target is not null)
            {
                ExitState();
                _player.MoveState.SetDestination(_player.Target.transform.position);
                _player.MoveState.EnterState();
            }
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