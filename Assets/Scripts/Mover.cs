using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{

    private NavMeshAgent _agent;
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private LayerMask _interactableLayer;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimator();

        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, _interactableLayer))
            {
                _agent.destination = hit.point;
            }
        }
    }

    private void UpdateAnimator()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(_agent.velocity);
        _animator.SetFloat("speed", localVelocity.z);
    }
}
