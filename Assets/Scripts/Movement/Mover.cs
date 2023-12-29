using com.ARTillery.Interfaces;
using UnityEngine;
using UnityEngine.AI;



namespace com.ARTillery.Movement
{
    public class Mover : MonoBehaviour
    {

        private NavMeshAgent _agent;
        [SerializeField]
        private Animator _animator;
        private IAnimationMaster _animationMaster;


        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animationMaster = GetComponent<IAnimationMaster>();
            _animationMaster.SetAnimator(_animator);
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 localVelocity = transform.InverseTransformDirection(_agent.velocity);
            _animationMaster.SetCharacterSpeed(localVelocity.z);
        }

        public void MoveTo(Vector3 destination)
        {
            _agent.destination = destination;
            _agent.isStopped = false;
        }


        public void Stop()
        {
            _agent.isStopped = true;
        }

        public bool IsReachedDestination()
        {
            return (_agent.remainingDistance <= _agent.stoppingDistance);
        }


        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.DrawLine(transform.position, _agent.destination);
            }
        }

        public bool HasPath(Vector3 position)
        {
            var navMeshPath = new NavMeshPath();
            return _agent.CalculatePath(position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete;
        }
    }
}
