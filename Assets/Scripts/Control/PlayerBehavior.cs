using com.ARTillery.Movement;
using UnityEngine;
using UnityEngine.AI;


namespace com.ARTillery.Control
{
    public class PlayerBehavior : MonoBehaviour
    {

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

        private Mover _mover;


        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(1))
            {
                MoveToCursor();
            }
            Interact();
        }

        private void MoveToCursor()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, _interactableLayer))
            {
                _mover.MoveTo(hit.point);
            }
        }

        private void Interact()
        {

        }

    }
}
