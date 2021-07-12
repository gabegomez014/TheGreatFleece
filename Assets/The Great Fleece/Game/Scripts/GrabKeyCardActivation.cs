using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabKeyCardActivation : MonoBehaviour
{
    public GameObject grabCardCutscene;

    private bool _cutScenePlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !_cutScenePlayed)
        {
            grabCardCutscene.SetActive(true);
            GameManager.Instance.HasCard = true;
            _cutScenePlayed = true;
        }
    }
}
