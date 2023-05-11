using com.ARTillery.Combat;
using com.ARTillery.Core;
using UnityEngine;

namespace com.ARTillery.Control
{
    public class PlayerCombatState : PlayerBaseState
    {
        private PlayerBehavior _player;
        private Fighter _fighter;
        private float _timer;

        public PlayerCombatState(PlayerBehavior player)
        {
            _player = player;
        }

        public override void EnterState()
        {
            _player.SetCurrentState(this);
            Debug.Log("Entering Combat State");
            _fighter ??= _player.Fighter; // if fighter is null, re-assign it
            _timer = float.MaxValue;
        }

        public override void UpdateState()
        {
            if (Input.GetMouseButton(1))
            {
                TargetType targetType = ClickTargetFinder.GetClickTargetType(out RaycastHit target);
                Debug.Log(target.transform.gameObject.name);
                switch (targetType)
                {
                    case TargetType.CombatTarget:
                        _player.SetCombatTarget(target.transform.GetComponent<CombatTarget>());
                        MoveToTarget(target.transform.position);
                        break;
                    case TargetType.ResourceNode:
                        _player.ClearCombatTarget();
                        _player.SetResourceNode(target.transform.GetComponent<ResourceNode>());
                        MoveToTarget(target.transform.position);
                        break;
                    case TargetType.ReachableLocation:
                        _player.ClearCombatTarget();
                        _player.ClearResourceNode();
                        MoveToTarget(target.point);
                        break;
                }
            }

            if (_timer >= _fighter.AttackRate)
            {
                _player.GetCombatTarget().TakeDamage(_fighter.AttackPower);
                //TODO: check if target is dead to get out of state
                if (_player.GetCombatTarget().IsCombatTargetDead())
                {
                    
                    _player.GetCombatTarget().DestroyCombatTarget();
                    _player.ClearCombatTarget();
                    ExitState();
                    _player.IdleState.EnterState();
                }
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