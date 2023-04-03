using com.ARTillery.Combat;
using UnityEngine;

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
                RaycastHit[] hits;
                if (_player.IsCursorHit(out hits))
                {
                    CombatTarget target;
                    if (_player.DetectCombatTarget(hits, out target))
                    {
                        _player.SetCombatTarget(target);
                        MoveToTarget(target.transform.position);
                    }
                    else
                    {
                        _player.ClearCombatTarget();
                        foreach (RaycastHit hit in hits)
                        {
                            if (_player.Mover.HasPath(hit.point))
                            {
                                MoveToTarget(hit.point);
                            }
                        }
                    }
                }
            }
        }

        private void MoveToTarget(Vector3 position)
        {
            _player.MoveState.SetDestination(position);
            ExitState();
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