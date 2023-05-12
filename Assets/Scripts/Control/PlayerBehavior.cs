using com.ARTillery.Combat;
using com.ARTillery.Movement;
using System;
using com.ARTillery.Inventory;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;


namespace com.ARTillery.Control
{
    public class PlayerBehavior : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] private string _currentStateName;

        [SerializeField] private Animator _animator;
        
        [SerializeField] private Transform _defaultCursor;
        [SerializeField] private Transform _combatCursor;
        [SerializeField] private Transform _miningCursor;

        [Header("Combat")]
        [SerializeField]
        private int weaponPower = 10;

        [Header("Gathering")]
        [SerializeField]
        private float _gatheringRange = 0.5f;

        [SerializeField]
        private int _gatheringPower = 5;

        [SerializeField]
        private float _gatheringRate = 0.5f;

        private Mover _mover;
        private Fighter _fighter;
        private CombatTarget _target;
        private ResourceNode _resourceNode;
        private NavMeshAgent _agent;


        public Action<ResourceType, int> OnResourceGathered;

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
        public float GatheringRate { get => _gatheringRate; set => _gatheringRate = value; }
        public int GatheringPower { get => _gatheringPower; set => _gatheringPower = value; }

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
            
            DisplayDefaultCursor();
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
            if (IsCursorHit(out var hits))
            {
                if (DetectCursorTarget(hits, out var target))
                {
                    //Debug.Log(target.GetGameObject().name);
                    if (target?.GetType() == typeof(CombatTarget))
                    {
                        DisplaySpecialCursor(CursorType.Combat);
                    }
                    else if (target?.GetType() == typeof(ResourceNode))
                    {
                        DisplaySpecialCursor(CursorType.Mining);
                    }
                }
                else
                {
                    DisplayDefaultCursor();
                }
            }
        }

        private void DisplayDefaultCursor()
        {
            Cursor.visible = false;
            _combatCursor.gameObject.SetActive(false);
            _miningCursor.gameObject.SetActive(false);
            _defaultCursor.gameObject.SetActive(true);
            _defaultCursor.position = Input.mousePosition;
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
            if (_resourceNode is null)
            {
                return;
            }
            _resourceNode = null;
        }

        public void SetResourceNode(ResourceNode target)
        {
            ClearResourceNode();
            _resourceNode = target;
        }

        public ResourceNode GetResourceNode()
        {
            return ResourceNode;
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

        public bool DetectCursorTarget(RaycastHit[] hits, out ICursorTarget target)
        {
            ICursorTarget cursorTarget = null;
            bool cursorTargetPresent = false;
            foreach (RaycastHit hit in hits)
            {
                ICursorTarget component;
                if (hit.transform.TryGetComponent<ICursorTarget>(out component))
                {
                    cursorTargetPresent = true;
                    cursorTarget = component;
                }
            }
            target = cursorTarget;
            
            return cursorTargetPresent;
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
        
        private void DisplaySpecialCursor(CursorType type)
        {
            _defaultCursor.gameObject.SetActive(false);
            switch (type)
            {
                case CursorType.Combat:
                    _combatCursor.gameObject.SetActive(true);
                    _combatCursor.position = Input.mousePosition;
                    break;
                case CursorType.Mining:
                    _miningCursor.gameObject.SetActive(true);
                    _miningCursor.position = Input.mousePosition;
                    break;
            }
            
        }

        public void DestroySourceNode()
        {
            ResourceNode.DestroySourceNode();
            ResourceNode = null;
        }
    }
}
