using com.ARTillery.Combat;
using com.ARTillery.Core;
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

            if (_player.ResourceNode is not null)
            {
                if (IsInGatheringRange())
                {
                    GatherResource();
                }
            }

            if (_player.Mover.IsReachedDestination())
            {
                _player.IdleState.EnterState();
            }


            if (Input.GetMouseButton(1))
            {
                TargetType targetType = ClickTargetFinder.GetClickTargetType(out RaycastHit target);
                Debug.Log(target.transform.gameObject.name);
                switch (targetType)
                {
                    case TargetType.CombatTarget:
                        _player.SetCombatTarget(target.transform.GetComponent<CombatTarget>());
                        SetDestination(target.transform.position);
                        break;
                    case TargetType.ResourceNode:
                        _player.ClearCombatTarget();
                        _player.SetResourceNode(target.transform.GetComponent<ResourceNode>());
                        SetDestination(target.transform.position);
                        break;
                    case TargetType.ReachableLocation:
                        _player.ClearCombatTarget();
                        _player.ClearResourceNode();
                        SetDestination(target.point);
                        break;
                }
            }
        }

        private void AttackTarget()
        {
            _player.Mover.Stop();
            ExitState();
            _player.CombatState.EnterState();
        }

        private void GatherResource()
        {
            _player.Mover.Stop();
            ExitState();
            _player.GatherState.EnterState();
        }

        private bool IsInCombatRange()
        {
            return Vector3.Distance(_player.transform.position, _player.GetCombatTarget().transform.position) <= _player.Fighter.Range;
        }

        private bool IsInGatheringRange()
        {
            return Vector3.Distance(_player.transform.position, _player.ResourceNode.transform.position) <= _player.GatheringRange;
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