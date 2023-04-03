using com.ARTillery.Combat;
using UnityEngine;

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

            if (_player.GetCombatTarget() is not null)
            {
                if (IsInCombatRange())
                {
                    AttackTarget();
                }
            }
            else
            {
                if (_player.Mover.IsReachedDestination())
                {
                    _player.IdleState.EnterState();
                }
            }

            if (Input.GetMouseButton(1))
            {
                RaycastHit[] hits;
                if (_player.IsCursorHit(out hits))
                {
                    CombatTarget target;
                    if (_player.DetectCombatTarget(hits, out target))
                    {
                        _player.SetCombatTarget(target);
                        SetDestination(target.transform.position);
                    }
                    else
                    {
                        _player.ClearCombatTarget();
                        foreach (RaycastHit hit in hits)
                        {
                            if (_player.Mover.HasPath(hit.point))
                            {
                                SetDestination(hit.point);
                            }
                        }
                    }
                }
            }
        }

        private void AttackTarget()
        {
            _player.Mover.Stop();
            ExitState();
            _player.CombatState.EnterState();
        }

        private bool IsInCombatRange()
        {
            return Vector3.Distance(_player.transform.position, _player.GetCombatTarget().transform.position) <= _player.Fighter.Range;
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