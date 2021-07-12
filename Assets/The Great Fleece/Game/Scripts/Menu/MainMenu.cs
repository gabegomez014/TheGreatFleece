using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource musicAudio;

    private void Awake()
    {
        musicAudio.Play();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LoadingScreen");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
