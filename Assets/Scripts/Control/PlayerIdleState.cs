using com.ARTillery.Combat;
using com.ARTillery.Core;
using Unity.Burst.CompilerServices;
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
                TargetType targetType = ClickTargetFinder.GetClickTargetType(out RaycastHit target);
                Debug.Log(target.transform.gameObject.name);
                switch (targetType)
                {
                    case TargetType.CombatTarget:
                        _player.SetCombatTarget(target.transform.GetComponent<CombatTarget>());
                        MoveToTarget(target.transform.position);
                        break;
                    case TargetType.ResourceNode:
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