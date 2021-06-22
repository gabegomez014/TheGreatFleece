using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardEyes : MonoBehaviour
{
    public GameObject gameOverCutscene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioManager.Instance.StopMusic();
            gameOverCutscene.SetActive(true);
        }
    }
}
