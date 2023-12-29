using com.ARTillery.Combat;
using com.ARTillery.Movement;
using System;
using com.ARTillery.Inventory;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace com.ARTillery.Control
{
    public class PlayerBehavior : MonoBehaviour
    {
        [Header("Setup")] 
        
        [SerializeField] private string currentStateName;

        [FormerlySerializedAs("_defaultCursor")] [SerializeField] private Transform defaultCursor;
        [FormerlySerializedAs("_combatCursor")] [SerializeField] private Transform combatCursor;
        [FormerlySerializedAs("_miningCursor")] [SerializeField] private Transform miningCursor;

        [Header("Combat")]
        [SerializeField]
        private int weaponPower = 10;

        [FormerlySerializedAs("_gatheringRange")]
        [Header("Gathering")]
        [SerializeField]
        private float gatheringRange = 0.5f;

        [FormerlySerializedAs("_gatheringPower")] [SerializeField]
        private int gatheringPower = 5;

        [FormerlySerializedAs("_gatheringRate")] [SerializeField]
        private float gatheringRate = 0.5f;

        private Mover _mover;
        private Fighter _fighter;
        private CombatTarget _target;
        private ResourceNode _resourceNode;
        private NavMeshAgent _agent;


        public Action<ResourceType, int> OnResourceGathered;

        private PlayerBaseState _currentState;

        private PlayerIdleState _idleState;
        private PlayerMoveState _moveState;
        private PlayerCombatState _combatState;
        private PlayerGatherState _gatherState;
        private PlayerBuildState _buildState;
        private PlayerDeathState _deathState;

        public PlayerIdleState IdleState { get => _idleState; set => _idleState = value; }
        public PlayerMoveState MoveState { get => _moveState; set => _moveState = value; }
        public PlayerCombatState CombatState { get => _combatState; set => _combatState = value; }
        public PlayerGatherState GatherState { get => _gatherState; set => _gatherState = value; }
        public PlayerBuildState BuildState { get => _buildState; set => _buildState = value; }
        public PlayerDeathState DeathState { get => _deathState; set => _deathState = value; }
        public NavMeshAgent Agent { get => _agent; set => _agent = value; }
        public Fighter Fighter { get => _fighter; set => _fighter = value; }
        public Mover Mover { get => _mover; set => _mover = value; }
        public ResourceNode ResourceNode { get => _resourceNode; set => _resourceNode = value; }
        public float GatheringRange { get => gatheringRange; set => gatheringRange = value; }
        public float GatheringRate { get => gatheringRate; set => gatheringRate = value; }
        public int GatheringPower { get => gatheringPower; set => gatheringPower = value; }

        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            Mover = GetComponent<Mover>();
            Fighter = GetComponent<Fighter>();

            _idleState = new PlayerIdleState(this);
            _moveState = new PlayerMoveState(this);
            _combatState = new PlayerCombatState(this);
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
            currentStateName = _currentState.ToString();
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
            combatCursor.gameObject.SetActive(false);
            miningCursor.gameObject.SetActive(false);
            defaultCursor.gameObject.SetActive(true);
            defaultCursor.position = Input.mousePosition;
        }

        public Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) return;
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(MoveState.GetDestination(), 1);

        }

        public void SetCurrentState(PlayerBaseState state)
        {
            _currentState = state;
        }

        public void ClearCombatTarget()
        {
            if (_target is not null)
            {
                _target.ClearSelectedVisual();
            }

            _target = null;
        }

        public void SetCombatTarget(CombatTarget target)
        {
            ClearCombatTarget();
            target.SetSelectedVisual();
            _target = target;
        }

        public void ClearResourceNode()
        {
            if (_resourceNode is not null)
            {
                _resourceNode.ClearSelectedVisual();
            }
            _resourceNode = null;
        }

        public void SetResourceNode(ResourceNode target)
        {
            ClearResourceNode();
            target.SetSelectedVisual();
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
            return null;
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
        private void DisplaySpecialCursor(CursorType type)
        {
            defaultCursor.gameObject.SetActive(false);
            switch (type)
            {
                case CursorType.Combat:
                    combatCursor.gameObject.SetActive(true);
                    combatCursor.position = Input.mousePosition;
                    break;
                case CursorType.Mining:
                    miningCursor.gameObject.SetActive(true);
                    miningCursor.position = Input.mousePosition;
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
