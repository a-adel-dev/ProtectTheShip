using com.ARTillery.Combat;
using com.ARTillery.Movement;
using System;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;


namespace com.ARTillery.Control
{
    public class PlayerBehavior : MonoBehaviour
    {

        [SerializeField]
        private string _currentStateName;

        [SerializeField]
        private int _harvestValue = 10;

        [SerializeField]
        private int weaponPower = 10;

        [SerializeField]
        private float _harvestingTimer = 2f;

        private NavMeshAgent _agent;
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private LayerMask _interactableLayer;

        [SerializeField]
        private Transform _combatCursor;

        [SerializeField]
        private float _gatheringRange = 0.5f;

        private Mover _mover;
        private Fighter _fighter;
        private CombatTarget _target;
        private ResourceNode _resourceNode;


        private PlayerBaseState _currentState;

        private PlayerIdleState _idleState;
        private PlayerMoveState _moveState;
        private PlayerCombatState _CombatState;
        private PlayerGatherState _gatherState;
        private PlayerBuildState _buildState;
        private PlayerDeathState _deathState;

        public PlayerIdleState IdleState { get => _idleState; set => _idleState = value; }
        public PlayerMoveState MoveState { get => _moveState; set => _moveState = value; }
        public PlayerCombatState CombatState { get => _CombatState; set => _CombatState = value; }
        public PlayerGatherState GatherState { get => _gatherState; set => _gatherState = value; }
        public PlayerBuildState BuildState { get => _buildState; set => _buildState = value; }
        public PlayerDeathState DeathState { get => _deathState; set => _deathState = value; }
        public NavMeshAgent Agent { get => _agent; set => _agent = value; }
        public Fighter Fighter { get => _fighter; set => _fighter = value; }
        public Mover Mover { get => _mover; set => _mover = value; }
        public ResourceNode ResourceNode { get => _resourceNode; set => _resourceNode = value; }
        public float GatheringRange { get => _gatheringRange; set => _gatheringRange = value; }

        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            Mover = GetComponent<Mover>();
            Fighter = GetComponent<Fighter>();

            _idleState = new PlayerIdleState(this);
            _moveState = new PlayerMoveState(this);
            _CombatState = new PlayerCombatState(this);
            _gatherState = new PlayerGatherState(this);
            _buildState = new PlayerBuildState(this);
            _deathState = new PlayerDeathState(this);

            _currentState = IdleState;
            EnterState(_currentState);
        }

        public void EnterState(PlayerBaseState state)
        {
            state.EnterState();
            Debug.Log("changing state");
        }

        private void Update()
        {
            _currentState.UpdateState();
            _currentStateName = _currentState.ToString();
            InteractWithCombatCursor();

        }

        private void InteractWithCombatCursor()
        {
            RaycastHit[] hits;
            CombatTarget target;
            if (IsCursorHit(out hits))
            {
                if (DetectCombatTarget(hits, out target))
                {
                    DisplayCombatCursor();
                }
                else
                {
                    _combatCursor.gameObject.SetActive(false);
                    Cursor.visible = true;
                }
            }
        }

        public Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(MoveState.GetDestination(), 1);
            }

        }

        public void SetCurrentState(PlayerBaseState state)
        {
            _currentState = state;
        }

        public void ClearCombatTarget()
        {
            _target?.ClearTargetVisual();
            _target = null;
        }

        public void SetCombatTarget(CombatTarget target)
        {
            ClearCombatTarget();
            _target = target;
            _target.SetTargetVisual();
        }

        public void ClearResourceNode()
        {
            _resourceNode = null;
        }

        public void SetResourceNode(ResourceNode target)
        {
            ClearResourceNode();
            _resourceNode = target;
        }

        public CombatTarget GetCombatTarget()
        {
            if (_target is not null)
            {
                return _target;
            }
            else
            {
                return null;
            }
        }

        public bool DetectCombatTarget(RaycastHit[] hits, out CombatTarget target)
        {
            CombatTarget combatTarget = null;
            bool combatTargetPresent = false;
            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.GetComponent<CombatTarget>())
                {
                    combatTarget = hit.transform.GetComponent<CombatTarget>();
                    combatTargetPresent = true;
                }
            }
            target = combatTarget;
            return combatTargetPresent;
        }

        public bool IsCursorHit(out RaycastHit[] targetHits)
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            if (hits.Length == 0)
            {
                //Debug.Log("nothing to do");
                targetHits = null;
                return false;
            }
            else
            {
                targetHits = hits;
                return true;
            }
        }


        private void DisplayCombatCursor()
        {
            _combatCursor.gameObject.SetActive(true);
            Cursor.visible = false;
            _combatCursor.position = Input.mousePosition;
        }
    }
}
