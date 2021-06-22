using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public Image progressBar;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadLevelAsync());
    }

    IEnumerator LoadLevelAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Main");

        while (!operation.isDone)
        {
            progressBar.fillAmount = operation.progress;
            Debug.Log("Progress is " + operation.progress);
            yield return new WaitForEndOfFrame();
        }
    }
}
