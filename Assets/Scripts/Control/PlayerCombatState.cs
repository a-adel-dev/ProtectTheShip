using com.ARTillery.Combat;
using UnityEngine;

namespace com.ARTillery.Control
{
    public class PlayerCombatState : PlayerBaseState
    {
        private PlayerBehavior _player;
        private Fighter _fighter;
        private float _timer = float.MaxValue;

        public PlayerCombatState(PlayerBehavior player)
        {
            _player = player;
        }

        public override void EnterState()
        {
            _player.SetCurrentState(this);
            Debug.Log("Entering Combat State");
            _fighter ??= _player.Fighter; // if fighter is null, re-assign it
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

            if (_player.GetCombatTarget() is null)
            {
                _timer = float.MaxValue;
                return;
            }

            if (_timer >= _fighter.AttackRate)
            {
                _player.GetCombatTarget().TakeDamage(_fighter.AttackPower);
                _timer = 0;
                Debug.Log("Attacking!");
            }

            _timer += Time.deltaTime;
        }

        public override void ExitState()
        {

        }

        private void MoveToTarget(Vector3 position)
        {
            _player.MoveState.SetDestination(position);
            ExitState();
            _player.MoveState.EnterState();
        }

        public override string ToString()
        {
            return "Combat";
        }
    }
}