using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{

    private NavMeshAgent _agent;
    private Animator _animator;
    private Vector3 _target;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayOrigin, out hit, 100))
            {
                _agent.SetDestination(hit.point);
                _target = _agent.destination;
                _animator.SetBool("Walk", true);
            }
        }

        float distance = Vector3.Distance(transform.position, _target);
        if (distance <= 2)
        {
            _animator.SetBool("Walk", false);
        }
    }
}
