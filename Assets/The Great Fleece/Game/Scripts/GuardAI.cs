using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    public List<Transform> wayPoints;

    private Transform _target;
    private NavMeshAgent _agent;
    private Animator _animator;
    private bool _reverse = false;
    private bool _endReached = false;
    private bool _coinTossed = false;
    private int _currentWaypoint;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        if (_animator == null)
        {
            Debug.LogWarning("Could not find animator component");
        }

        if (_agent == null)
        {
            Debug.LogWarning("Could not find NavMeshAgent component");
        }

        if (wayPoints.Count != 0 && wayPoints[0] != null)
        {
            _animator.SetBool("Walk", true);
            _currentWaypoint = 1;
            _target = wayPoints[_currentWaypoint]; // Set target to the second way point in the list since it is assumed they start at the first waypoint
            _agent.SetDestination(_target.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null) { return; }

        if (!_coinTossed)
        {
            float distanceToTarget = Vector3.Distance(transform.position, _target.position);

            if (distanceToTarget <= 3 && _endReached == false)
            {
                if (_reverse)
                {
                    if ((_currentWaypoint - 1) < 0 || wayPoints[_currentWaypoint - 1] == null)
                    {
                        _endReached = true;
                        _animator.SetBool("Walk", false);
                        StartCoroutine(WaitBeforeMoving());
                        return;
                    }

                    else
                    {
                        _currentWaypoint -= 1;
                        _target = wayPoints[_currentWaypoint];
                        _agent.SetDestination(_target.position);
                    }
                }

                else
                {
                    if (_currentWaypoint + 1 >= wayPoints.Count || wayPoints[_currentWaypoint + 1] == null)
                    {
                        _endReached = true;
                        _animator.SetBool("Walk", false);
                        StartCoroutine(WaitBeforeMoving());
                        return;
                    }

                    else
                    {
                        _currentWaypoint += 1;
                        _target = wayPoints[_currentWaypoint];
                        _agent.SetDestination(_target.position);
                    }
                }
            }
        }

        else
        {
            float distanceToTarget = Vector3.Distance(transform.position, _target.position);

            if (distanceToTarget <= 5)
            {
                _agent.isStopped = true;
                _animator.SetBool("Walk", false);
            }
        }
    }

    public void CoinTossed(Transform coinPosition)
    {
        _coinTossed = true;
        _target = coinPosition;
        _agent.SetDestination(coinPosition.position);
        _animator.SetBool("Walk", true);
    }

    IEnumerator WaitBeforeMoving()
    {
        yield return new WaitForSeconds(Random.Range(2, 5));
        _reverse = !_reverse;
        _endReached = false;
        _animator.SetBool("Walk", true);
    }
}
