using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager is null");
            }

            return _instance;
        }
    }

    public bool HasCard { get; set; }

    [SerializeField]
    private LookAtPlayer _lookAtPlayer;
    [SerializeField]
    private PlayableDirector _introCutsceneDirector;
    [SerializeField]
    private GameObject _introCutscene;
    private bool _musicPlayed;

    private void Awake()
    {
        _instance = this;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && _introCutsceneDirector.state == PlayState.Playing)
        {
            _introCutsceneDirector.time = 60.0f;
        }

        if (_introCutsceneDirector.time >= 60 && !_musicPlayed)
        {
            AudioManager.Instance.PlayMusic();
            _musicPlayed = true;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _introCutscene.SetActive(true);
    }
}
