using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{ 
    public Transform target;
    public Transform startTransform;

    private void Start()
    {
        transform.position = startTransform.position;
        transform.rotation = startTransform.rotation; 
    }

    // Update is called once per frame
    void Update()
    {
        if (target.gameObject.activeSelf)
        {
            transform.LookAt(target);
        }
    }
}
