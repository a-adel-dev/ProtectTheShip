using com.ARTillery.Combat;
using com.ARTillery.Core;
using com.ARTillery.Inventory;
using UnityEngine;

namespace com.ARTillery.Control
{
    public class PlayerGatherState : PlayerBaseState
    {
        private PlayerBehavior _player;
        private float _timer;

        private ResourceNode _currentNode;
        public PlayerGatherState(PlayerBehavior player)
        {
            _player = player;
        }

        public override void EnterState()
        {
            _player.SetCurrentState(this);
            Debug.Log($"Entering Gather State");
            _timer = float.MaxValue;
            _player.transform.LookAt(_player.GetResourceNode().transform);
            _currentNode = _player.GetResourceNode();
            //_currentNode.SetSelectedVisual();
        }

        public override void UpdateState()
        {
            if (Input.GetMouseButton(1))
            {
                TargetType targetType = ClickTargetFinder.GetClickTargetType(out RaycastHit target);
                //Debug.Log(target.transform.gameObject.name);
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

            if (_timer >= _player.GatheringRate)
            {
                ResourceType type;
                int amountGathered = _player.GetResourceNode().HarvestNode(_player.GatheringPower, out type);
                _player.OnResourceGathered?.Invoke(type, amountGathered);
                if (_player.GetResourceNode().IsResourceExhausted)
                {
                    _player.DestroySourceNode();
                    _player.IdleState.EnterState();
                    return;
                }
                _timer = 0;
            }

            _timer += Time.deltaTime;
        }

        public override void ExitState()
        {
            //_currentNode.ClearSelectedVisual();
        }

        private void MoveToTarget(Vector3 position)
        {
            _player.MoveState.SetDestination(position);
            ExitState();
            _player.MoveState.EnterState();
        }

        public override string ToString()
        {
            return "Gathering";
        }
    }
}