using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace com.ARTillery.Movement
{
    public class Mover : MonoBehaviour
    {

        private NavMeshAgent _agent;
        [SerializeField]
        private Animator _animator;


        void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }

        public void MoveTo(Vector3 destination)
        {
            Debug.Log("setting destination");
            _agent.destination = destination;
            _agent.isStopped = false;
        }

        private void UpdateAnimator()
        {
            Vector3 localVelocity = transform.InverseTransformDirection(_agent.velocity);
            _animator.SetFloat("speed", localVelocity.z);
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
    }
}
