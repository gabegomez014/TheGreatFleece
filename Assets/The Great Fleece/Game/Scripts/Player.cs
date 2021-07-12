using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _coinPrefab;
    [SerializeField]
    private AudioClip _coinSFX;

    private NavMeshAgent _agent;
    private Animator _animator;
    private Vector3 _target;

    private bool _coinTossed = false;

    private GameObject _instantiatedCoin;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = transform.GetChild(0).GetComponent<Animator>();
        _instantiatedCoin = Instantiate(_coinPrefab);
        _instantiatedCoin.SetActive(false);
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

        if (Input.GetMouseButtonDown(1) && !_coinTossed)
        {
            RaycastHit hit;

            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(rayOrigin, out hit, 100))
            {
                _coinTossed = true;
                Vector3 dropPosition = hit.point;
                _animator.SetTrigger("Throw");
                _instantiatedCoin.transform.position = dropPosition;
                _instantiatedCoin.SetActive(true);
                AudioSource.PlayClipAtPoint(_coinSFX, _instantiatedCoin.transform.position);
                StartCoroutine(CoinTossed());
                SendGuardsToCoin(_instantiatedCoin.transform);
            }
        }
    }

    IEnumerator CoinTossed()
    {
        yield return new WaitForSeconds(2);
        _coinTossed = false;
    }

    void SendGuardsToCoin(Transform coinPosition)
    {
        GameObject[] guards = GameObject.FindGameObjectsWithTag("Guard1");
        foreach(var guard in guards)
        {
            GuardAI guardAgent = guard.GetComponent<GuardAI>();
            guardAgent.CoinTossed(coinPosition);
        }
    }
}
