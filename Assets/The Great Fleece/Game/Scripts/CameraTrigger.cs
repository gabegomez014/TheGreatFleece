using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Transform camToSnapTo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Camera.main.transform.position = camToSnapTo.position;
            Camera.main.transform.rotation = camToSnapTo.rotation;
        }
    }
}
