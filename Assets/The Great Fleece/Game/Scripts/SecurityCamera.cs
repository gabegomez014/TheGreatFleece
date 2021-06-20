using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public GameObject gameOverCutscene;

    private Animator _animator;

    private void Start()
    {
        _animator = transform.parent.parent.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            MeshRenderer render = GetComponent<MeshRenderer>();
            Color color = new Color(0.6f, 0.1f, 0.1f, .3f);
            render.material.SetColor("_TintColor",color);
            _animator.enabled = false;
            StartCoroutine(WaitBeforeGameOver());
        }
    }

    IEnumerator WaitBeforeGameOver()
    {
        yield return new WaitForSeconds(0.5f);
        gameOverCutscene.SetActive(true);
    }
}
