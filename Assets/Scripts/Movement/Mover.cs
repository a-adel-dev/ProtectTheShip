using com.ARTillery.Interfaces;
using Pathfinding;
using UnityEngine;
using UnityEngine.AI;



namespace com.ARTillery.Movement
{
    public class Mover : MonoBehaviour
    {

        private FollowerEntity _agent;
        [SerializeField]
        private Animator _animator;
        private IAnimationMaster _animationMaster;


        void Start()
        {
            _agent = GetComponent<FollowerEntity>();
            _animationMaster = GetComponent<IAnimationMaster>();
            _animationMaster.SetAnimator(_animator);
        }

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
            return (_agent.remainingDistance <= _agent.stopDistance);
        }


        private void OnDrawGizmos()
        {
            if (Application.isPlaying)
            {
                Gizmos.DrawLine(transform.position, _agent.destination);
            }
        }

        // public bool HasPath(Vector3 position)
        // {
        //     var navMeshPath = new NavMeshPath();
        //     return _agent.CalculatePath(position, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete;
        // }

        public void LookAtTarget(Transform target)
        {
            transform.LookAt(new Vector3(target.position.x, transform.forward.y, target.position.z));   
        }
    }
}
