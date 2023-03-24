using com.ARTillery.Combat;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace com.ARTillery.Control
{
    public class PlayerMoveState : PlayerBaseState
    {
        private PlayerBehavior _player;
        private Vector3 _destination;

        private bool _dirtyDestination = true;
        public PlayerMoveState(PlayerBehavior player)
        {
            _player = player;
        }

        public override void EnterState()
        {
            Debug.Log($"Entering Move State");
            _player.SetCurrentState(this);
        }

        public override void UpdateState()
        {
            if (_dirtyDestination)
            {
                _player.Mover.MoveTo(_destination);
                _dirtyDestination = false;
            }


            if (_player.Target is not null)
            {
                if (Vector3.Distance(_player.transform.position, _player.Target.transform.position) <= _player.Fighter.Range)
                {
                    _player.Mover.Stop();
                    ExitState();
                    _player.CombatState.EnterState();
                }   
            }

            if (_player.Mover.IsReachedDestination())
            {
                _player.IdleState.EnterState();
            }

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
                        _destination = _player.Target.transform.position;
                        _dirtyDestination = true;
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
                            _dirtyDestination = true;
                        }
                    }
                }
            }
        }

        public override void ExitState()
        {

        }

        public void SetDestination(Vector3 destination)
        {
            _destination = destination;
            _dirtyDestination = true;
        }

        public Vector3 GetDestination()
        {
            return _destination;
        }

        public override string ToString()
        {
            return "Moving";
        }
    }
}