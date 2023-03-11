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
    private float _stoppingDistance;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        if (_agent.remainingDistance >= _stoppingDistance)
        {
            _animator.SetBool("isMoving", true);
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                //Debug.DrawLine(Camera.main.transform.position, hit.point);
                //_agent.enabled = false;
                //transform.LookAt(new Vector3(hit.transform.position.x, _agent.transform.position.y, hit.transform.position.z));
                //_agent.enabled = true;
                _agent.destination = hit.point;
            }
        }
    }


}
