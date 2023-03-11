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
            _agent.destination = destination;
        }

        private void UpdateAnimator()
        {
            Vector3 localVelocity = transform.InverseTransformDirection(_agent.velocity);
            _animator.SetFloat("speed", localVelocity.z);
        }
    }
}
